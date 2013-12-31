using System;
using System.Collections.Generic;
using System.Linq;
using NMahjong.Aux;
using NMahjong.Base;
using NMahjong.Japanese;

using TA = NMahjong.Japanese.TileAnnotations;

namespace NMahjong.Drivers.Mjai
{
    internal class MjaiRuleAdvisor : IRuleAdvisor
    {
        private readonly IGameState mGameState;
        private readonly int mBaseId;

        private MjaiJson mMessage;

        private delegate TOpenMeld OpenMeldFactory<TOpenMeld>(IEnumerable<CanonicalTile> exposed,
                                                              CanonicalTile claimed,
                                                              PlayerId feeder)
            where TOpenMeld : RevealedMeld;

        internal MjaiRuleAdvisor(IGameState gameState, int baseId)
        {
            mGameState = gameState;
            mBaseId = baseId;
        }

        public int RequiredFan
        {
            get { return 1; }
        }

        public ImmutableList<IPlayerAction> GetValidActions()
        {
            var actions = new List<IPlayerAction>();

            if (mGameState.Turn == PlayerId.Self) {
                actions.AddRange(GetValidDiscards().Select(Actions.Discard));
            } else {
                actions.Add(Actions.None());
            }

            if (mMessage.Has("possible_actions")) {
                var possible = mMessage.Get<IEnumerable<MjaiJson>>("possible_actions");
                actions.AddRange(possible.Select(ParseToPlayerAction));
            }

            return actions.ToImmutableList();
        }

        internal void SetMessage(MjaiJson mjson)
        {
            mMessage = mjson;
        }

        [VisibleForTesting]
        internal IEnumerable<AnnotatedTile> GetValidDiscards()
        {
            IPlayerState player = mGameState.Players[PlayerId.Self];
            IEnumerable<AnnotatedTile> tiles = player.Tiles;
            if (player.HasRiichiDeclared()) {
                return tiles.Where(t => t.Has(TA.Drawn));
            }
            if (mMessage.Has("cannot_dahai")) {
                var prohibited = mMessage.Get<HashSet<CanonicalTile>>("cannot_dahai");
                tiles = tiles.Where(t => !prohibited.Contains(t.StripAnnotations()));
            }
            return tiles.Distinct();  // The order can be arbitrary.
        }

        [VisibleForTesting]
        internal IPlayerAction ParseToPlayerAction(MjaiJson mjson)
        {
            switch (mjson.Type) {
                case "pon":
                    return Actions.Pung(ParseToOpenMeld(mjson, OpenPung.Create));
                case "chi":
                    return Actions.Chow(ParseToOpenMeld(mjson, OpenChow.Create));
                case "daiminkan":
                    return Actions.OpenKong(ParseToOpenMeld(mjson, OpenKong.Create));
                case "ankan":
                    return Actions.ConcealedKong(ParseToConcealedKong(mjson));
                case "kakan":
                    return Actions.ExtendedKong(ParseToExtendedKong(mjson));
                case "reach":
                    return Actions.Riichi();
                case "hora":
                    return Actions.Mahjong();
                case "ryukyoku":
                    return Actions.AbortiveDraw();
                default:
                    throw new MjaiInvalidFieldException("type", mjson);
            }
        }

        private TOpenMeld ParseToOpenMeld<TOpenMeld>(MjaiJson mjson,
                                                     OpenMeldFactory<TOpenMeld> factory)
            where TOpenMeld : RevealedMeld
        {
            PlayerId feeder = new PlayerId((mjson.Get<Int32>("target") - mBaseId + 4) % 4);
            return factory(mjson.Get<IEnumerable<CanonicalTile>>("consumed"),
                           mjson.Get<CanonicalTile>("pai"), feeder);
        }

        private ConcealedKong ParseToConcealedKong(MjaiJson mjson)
        {
            return ConcealedKong.Create(mjson.Get<IEnumerable<CanonicalTile>>("consumed"));
        }

        private ExtendedKong ParseToExtendedKong(MjaiJson mjson)
        {
            var extender = mjson.Get<CanonicalTile>("pai");
            OpenPung basePung = mGameState.Players[PlayerId.Self].Melds.OfType<OpenPung>()
                .Single(pung => pung.Tiles[0] == extender.BaseTile);
            return ExtendedKong.Create(basePung, extender);
        }
    }
}
