using System;
using System.Collections.Generic;
using System.Linq;
using NMahjong.Aux;
using NMahjong.Base;

using TA = NMahjong.Japanese.TileAnnotations;

namespace NMahjong.Japanese
{
    using AnnotatedTileSeq = IEnumerable<AnnotatedTile>;
    using CanonicalTileSeq = IEnumerable<CanonicalTile>;

    internal static class RevealedMeldsHelper
    {
        internal static AnnotatedTileSeq ProcessTiles(
            CanonicalTileSeq exposedTiles,
            CanonicalTile claimedTile)
        {
            CheckArg.NotContainsNull(exposedTiles, "exposedTiles");
            CheckArg.NotNull(claimedTile, "claimedTile");
            var append = claimedTile.With(TA.Claimed);
            return exposedTiles.Cast<AnnotatedTile>().Append(append);
        }

        internal static AnnotatedTileSeq ProcessTiles(
            CanonicalTileSeq exposedTiles)
        {
            CheckArg.NotContainsNull(exposedTiles, "exposedTiles");
            return exposedTiles.Cast<AnnotatedTile>();
        }
    }
}
