using System;
using System.Collections.Generic;
using System.Linq;
using NMahjong.Aux;

namespace NMahjong.Base
{
    /**
      <summary>
        Provides extension methods for <see cref="Tile"/>.
      </summary>
    */
    public static class Tiles
    {
        private static readonly ImmutableDictionary<Tile, Int32>
            TileToIndex = BuildTileToIndex();

        private static ImmutableDictionary<Tile, Int32> BuildTileToIndex()
        {
            var tiles = Tile.AllTiles;
            return Enumerable.Range(0, tiles.Count).ToImmutableDictionary(j => tiles[j]);
        }

        /**
          <summary>
            Gets the index of the specified tile in <see cref="Tile.AllTiles"/>.
          </summary>
          <param name="tile">
            The tile to get the index of.
          </param>
          <returns>
            An integer <i>k</i> where <c>Tile.AllTiles[<i>k</i>]</c> equals to
            <paramref name="tile"/>.
          </returns>
        */
        public static int GetIndex(this Tile tile)
        {
            CheckArg.NotNull(tile, "tile");
            return TileToIndex[tile];
        }


        /**
          <summary>
            Determines whether the specified tile is a number tile.
          </summary>
          <param name="tile">
            The tile to investigate.
          </param>
          <returns>
            <see langword="true"/> if <paramref name="tile"/> is a number tile;
            otherwise, <see langword="false"/>.
          </returns>
        */
        public static bool IsNumberTile(this Tile tile)
        {
            CheckArg.NotNull(tile, "tile");
            return tile.TileType != TileType.Honor;
        }
    }
}
