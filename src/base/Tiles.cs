using System;
using System.Collections.Generic;
using System.Linq;
using NMahjong.Aux;

namespace NMahjong.Base
{
    public static class Tiles
    {
        private static readonly ImmutableDictionary<Tile, Int32>
            TileToIndex = BuildTileToIndex();

        private static ImmutableDictionary<Tile, Int32> BuildTileToIndex()
        {
            var tiles = Tile.AllTiles;
            return Enumerable.Range(0, tiles.Count).ToImmutableDictionary(j => tiles[j]);
        }

        public static int GetIndex(this Tile tile)
        {
            CheckArg.NotNull(tile, "tile");
            return TileToIndex[tile];
        }

        public static bool IsNumberTile(this Tile tile)
        {
            CheckArg.NotNull(tile, "tile");
            return tile.TileType != TileType.Honor;
        }
    }
}
