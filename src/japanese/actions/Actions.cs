using System;
using NMahjong.Aux;

namespace NMahjong.Japanese
{
    public static class Actions
    {
        public static IPlayerAction AbortiveDraw()
        {
            return AbortiveDrawAction.Instance;
        }

        public static IPlayerAction ConcealedKong(ConcealedKong kong)
        {
            return new ConcealedKongAction(kong);
        }

        public static IPlayerAction Chow(OpenChow chow)
        {
            return new ChowAction(chow);
        }

        public static IPlayerAction Discard(AnnotatedTile tile)
        {
            return new DiscardAction(tile);
        }

        public static IPlayerAction ExtendedKong(ExtendedKong kong)
        {
            return new ExtendedKongAction(kong);
        }

        public static IPlayerAction Mahjong()
        {
            return MahjongAction.Instance;
        }

        public static IPlayerAction None()
        {
            return NoneAction.Instance;
        }

        public static IPlayerAction OpenKong(OpenKong kong)
        {
            return new OpenKongAction(kong);
        }

        public static IPlayerAction Pung(OpenPung pung)
        {
            return new PungAction(pung);
        }

        public static IPlayerAction Riichi()
        {
            return RiichiAction.Instance;
        }

        public static bool AreIdentical(IPlayerAction a, IPlayerAction b)
        {
            CheckArg.NotNull(a, "a");
            CheckArg.NotNull(b, "b");

            if (a.GetType() != b.GetType()) {
                return false;
            }
            if (a is MeldAction) {
                return RevealedMelds.AreIdentical((a as MeldAction).Meld, (b as MeldAction).Meld);
            }
            if (a is DiscardAction) {
                return Object.Equals((a as DiscardAction).Tile, (b as DiscardAction).Tile);
            }

            return a == b;  // All other action objects are singletons.
        }
    }
}
