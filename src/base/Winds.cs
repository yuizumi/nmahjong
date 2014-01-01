using System.Collections.Generic;
using NMahjong.Aux;

namespace NMahjong.Base
{
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

        public static Tile GetTile(this Wind wind)
        {
            CheckArg.Enum(wind, "wind");
            return WindToTile[wind];
        }
    }
}
