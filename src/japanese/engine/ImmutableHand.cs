using System;
using System.Collections.Generic;
using System.Linq;
using NMahjong.Aux;
using NMahjong.Base;

using TA = NMahjong.Japanese.TileAnnotations;

namespace NMahjong.Japanese.Engine
{
    internal class ImmutableHand : ReadOnlyListView<AnnotatedTile>, IPlayerHandInternal
    {
        private const string MessageImmutable = "Hand cannot be altered.";

        private ImmutableHand(AnnotatedTile[] tiles)
            : base(tiles)
        {
        }

        internal static ImmutableHand ForWaiting(IEnumerable<CanonicalTile> tiles)
        {
            CheckArg.NotContainsNull(tiles, "tiles");
            return new ImmutableHand(tiles.Cast<AnnotatedTile>().ToArray());
        }

        internal static ImmutableHand ForWinning(IEnumerable<CanonicalTile> tiles,
                                                 Winning winning)
        {
            CheckArg.NotContainsNull(tiles, "tiles");
            CheckArg.NotEmpty(tiles, "tiles");
            CheckArg.Enum(winning, "winning");
            AnnotatedTile[] array = tiles.Cast<AnnotatedTile>().ToArray();
            int last = array.Length - 1;
            array[last] = array[last].With(winning == Winning.SelfDraw ? TA.Drawn : TA.Claimed);
            return new ImmutableHand(array);  // This array is not referenced outside.
        }

        public void Discard(AnnotatedTile tile)
        {
            throw new NotSupportedException(MessageImmutable);
        }

        public void Draw(CanonicalTile tile)
        {
            throw new NotSupportedException(MessageImmutable);
        }

        public void Exclude(IEnumerable<CanonicalTile> tiles)
        {
            throw new NotSupportedException(MessageImmutable);
        }
    }
}
