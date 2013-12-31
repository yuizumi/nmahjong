using System.Collections.Generic;
using NMahjong.Base;

namespace NMahjong.Japanese.Engine
{
    public static class PlayerHand
    {
        public static IPlayerHand Showed(ICollection<CanonicalTile> tiles)
        {
            return ShowedHand.Create(tiles);
        }

        public static IPlayerHand Hidden(int count)
        {
            return new HiddenHand(count);
        }

        public static IPlayerHand Waiting(ICollection<CanonicalTile> tiles)
        {
            return ImmutableHand.ForWaiting(tiles);
        }

        public static IPlayerHand Winning(ICollection<CanonicalTile> tiles, Winning winning)
        {
            return ImmutableHand.ForWinning(tiles, winning);
        }
    }
}
