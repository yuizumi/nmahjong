using System.Linq;
using NMahjong.Aux;
using NMahjong.Base;
using NMahjong.Japanese;
using NMahjong.Japanese.Engine;

using TA = NMahjong.Japanese.TileAnnotations;

namespace NMahjong.Drivers.Mjai
{
    internal class MjaiActionDecider
    {
        private readonly Intelligence mIntelligence;
        private MjaiAction mAction;

        private const string MessageInconsistentAction =
            "Intelligence returned an action inconsistent with the present state.";

        internal MjaiActionDecider(Intelligence intelligence)
        {
            mIntelligence = intelligence;
        }

        internal MjaiAction GetNextAction()
        {
            MjaiAction action = mAction;
            mAction = null;
            return action;
        }

        internal void RegisterHandlers(IEventHandlerRegisterer registerer)
        {
            registerer.AddOnMeldExtended(OnMeldExtended);
            registerer.AddOnMeldRevealed(OnMeldRevealed);
            registerer.AddOnRiichiDeclared(OnRiichiDeclared);
            registerer.AddOnTileDiscarded(OnTileDiscarded);
            registerer.AddOnTileDrawn(OnTileDrawn);
        }

        private void OnMeldExtended(object sender, MeldExtendedEventArgs e)
        {
            if (e.Player != PlayerId.Self) {
                IPlayerAction action = mIntelligence.OnKong(e.Player, e.Extender);
                mAction = CreateAction(action, e.Player, e.Extender);
            }
        }

        private void OnMeldRevealed(object sender, MeldRevealedEventArgs e)
        {
            // TODO(yuizumi): Allow robbing of a concealed kong for Thirteen Orphans
            // once it is implemented in the mjai server.
            if (e.Player == PlayerId.Self && !e.Meld.IsKong) {
                mAction = CreateAction(mIntelligence.OnTurn(), e.Player, null);
            }
        }

        private void OnRiichiDeclared(object sender, RiichiEventArgs e)
        {
            if (e.Player == PlayerId.Self) {
                mAction = CreateAction(mIntelligence.OnRiichi(), e.Player, null);
            }
        }

        private void OnTileDiscarded(object sender, TileDiscardedEventArgs e)
        {
            if (e.Player != PlayerId.Self) {
                IPlayerAction action = mIntelligence.OnDiscard(e.Player, e.Discard);
                mAction = CreateAction(action, e.Player, e.Discard.StripAnnotations());
            }
        }

        private void OnTileDrawn(object sender, TileDrawnEventArgs e)
        {
            if (e.Player == PlayerId.Self) {
                mAction = CreateAction(mIntelligence.OnTurn(), e.Player, e.Tile);
            }
        }

        [VisibleForTesting]
        internal static MjaiAction CreateAction(IPlayerAction action, PlayerId target,
                                                CanonicalTile playedTile)
        {
            CheckAction.NotNull(action);
            if (action is ChowAction || action is PungAction || action is OpenKongAction) {
                RevealedMeld meld = (action as MeldAction).Meld;
                CheckAction.Expect(meld.Feeder == target, MessageInconsistentAction);
                CanonicalTile claimed = meld.AnnotatedTiles
                    .Single(a => a.Has(TA.Claimed)).StripAnnotations();
                CheckAction.Expect(claimed == playedTile, MessageInconsistentAction);
            }
            return new MjaiAction(action, target, playedTile);
        }
    }
}
