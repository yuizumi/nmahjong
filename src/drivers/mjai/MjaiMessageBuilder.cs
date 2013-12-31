using System;
using System.Collections.Generic;
using System.Linq;
using NMahjong.Base;
using NMahjong.Japanese;

using TA = NMahjong.Japanese.TileAnnotations;

namespace NMahjong.Drivers.Mjai
{
    using TilePredicate = Func<AnnotatedTile, Boolean>;

    internal class MjaiMessageBuilder
    {
        private readonly int mBaseId;

        internal MjaiMessageBuilder(int baseId)
        {
            mBaseId = baseId;
        }

        internal string Build(MjaiAction mjaiAction)
        {
            return MjaiJson.Serialize(BuildInternal(mjaiAction));
        }

        private object BuildInternal(MjaiAction mjaiAction)
        {
            if (mjaiAction == null || mjaiAction.Action is NoneAction) {
                return new { type = "none" };
            }

            IPlayerAction action = mjaiAction.Action;

            if (action is DiscardAction) {
                return BuildForDiscard(mjaiAction);
            }
            if (action is RiichiAction) {
                return BuildForRiichi(mjaiAction);
            }
            if (action is MahjongAction) {
                return BuildForMahjong(mjaiAction);
            }
            if (action is AbortiveDrawAction) {
                return BuildForAbortiveDraw(mjaiAction);
            }
            if (action is PungAction) {
                return BuildForOpenMeld(mjaiAction, "pon");
            }
            if (action is ChowAction) {
                return BuildForOpenMeld(mjaiAction, "chi");
            }
            if (action is OpenKongAction) {
                return BuildForOpenMeld(mjaiAction, "daiminkan");
            }
            if (action is ConcealedKongAction) {
                return BuildForConcealedKong(mjaiAction);
            }
            if (action is ExtendedKongAction) {
                return BuildForExtendedKong(mjaiAction);
            }

            // TODO(yuizumi): Add AbortiveDraw once it gets supported in the mjai server.
            throw new NotSupportedException(action + " is not suported.");
        }

        private object BuildForDiscard(MjaiAction mjaiAction)
        {
            var action = mjaiAction.Action as DiscardAction;
            return new {
                type = "dahai", actor = mBaseId, pai = action.Tile.StripAnnotations(),
                tsumogiri = action.Tile.Has(TA.Drawn),
            };
        }

        private object BuildForRiichi(MjaiAction mjaiAction)
        {
            return new { type = "reach", actor = mBaseId };
        }

        private object BuildForMahjong(MjaiAction mjaiAction)
        {
            return new {
                type = "hora", actor = mBaseId, target = GetMjaiId(mjaiAction.Target),
                pai = mjaiAction.PlayedTile,
            };
        }

        private object BuildForAbortiveDraw(MjaiAction mjaiAction)
        {
            return new {
                type = "ryukyoku", actor = mBaseId,
                reason = DrawHand.NineTerminalsAndHonors,
            };
        }

        private object BuildForOpenMeld(MjaiAction mjaiAction, string type)
        {
            return new {
                type = type, actor = mBaseId, target = GetMjaiId(mjaiAction.Target),
                pai = mjaiAction.PlayedTile, consumed = GetConsumed(mjaiAction, TA.Claimed),
            };
        }

        private object BuildForConcealedKong(MjaiAction mjaiAction)
        {
            return new {
                type = "ankan", actor = mBaseId, consumed = GetConsumed(mjaiAction, 0),
            };
        }

        private object BuildForExtendedKong(MjaiAction mjaiAction)
        {
            return new {
                type = "kakan", actor = mBaseId, pai = GetPai(mjaiAction, TA.Extending),
                consumed = GetConsumed(mjaiAction, TA.Extending),
            };
        }

        private int GetMjaiId(PlayerId playerId)
        {
            return (playerId.Id + mBaseId) % 4;
        }

        private static CanonicalTile GetPai(MjaiAction mjaiAction, TileAnnotations selector)
        {
            return (mjaiAction.Action as MeldAction)
                .Meld.AnnotatedTiles.Single(a => a.Has(selector)).StripAnnotations();
        }

        private static IEnumerable<CanonicalTile> GetConsumed(
            MjaiAction mjaiAction, TileAnnotations excluder)
        {
            return (mjaiAction.Action as MeldAction)
                .Meld.AnnotatedTiles.Where(a => !a.Has(excluder)).Select(a => a.StripAnnotations());
        }
    }
}
