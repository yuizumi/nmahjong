using System.Collections.Generic;
using NMahjong.Aux;

namespace NMahjong.Base
{
    /**
      <summary>
        Provides extension methods for <see cref="Wind"/>.
      </summary>
    */
    public static class Winds
    {
        private static readonly ImmutableDictionary<Wind, Tile> WindToTile =
            ImmutableDictionary.Of(BuildWindToTile());

        private static Dictionary<Wind, Tile> BuildWindToTile()
        {
            return new Dictionary<Wind, Tile>() {
                {Wind.East, Tile.FE}, {Wind.South, Tile.FS}, {Wind.West, Tile.FW},
                {Wind.North, Tile.FN}
            };
        }

        /**
          <summary>
            Gets a tile corresponding to the specified wind.
          </summary>
          <param name="wind">
            The wind to get the tile for.
          </param>
          <returns>
            <see cref="Tile"/> that corresponds to <paramref name="wind"/>.
          </returns>
        */
        public static Tile GetTile(this Wind wind)
        {
            CheckArg.Enum(wind, "wind");
            return WindToTile[wind];
        }
    }
}
