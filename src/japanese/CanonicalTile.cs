using System.Collections.Generic;
using System.Linq;
using NMahjong.Aux;
using NMahjong.Base;

using TA = NMahjong.Japanese.TileAnnotations;

namespace NMahjong.Japanese
{
    public sealed class CanonicalTile : AnnotatedTile
    {
        private static readonly ImmutableDictionary<Tile, CanonicalTile>
            PlainTiles = ImmutableDictionary.Of(BuildMap(TA.None));
        private static readonly ImmutableDictionary<Tile, CanonicalTile>
            RedTiles = ImmutableDictionary.Of(BuildMap(TA.Red));

        private CanonicalTile(Tile baseTile, TileAnnotations annotations)
            : base(baseTile, annotations)
        {
        }

        public static CanonicalTile Plain(Tile baseTile)
        {
            return PlainTiles[baseTile];
        }

        public static CanonicalTile Red(Tile baseTile)
        {
            return RedTiles[baseTile];
        }

        private static IDictionary<Tile, CanonicalTile> BuildMap(
            TileAnnotations annotations)
        {
            return Tile.AllTiles.ToDictionary(
                tile => tile, tile => new CanonicalTile(tile, annotations));
        }
    }
}
