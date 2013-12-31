using System.Collections.Generic;
using System.Linq;
using NMahjong.Aux;
using NMahjong.Base;

namespace NMahjong.Japanese
{
    public static class DoraHelper
    {
        private static readonly ImmutableDictionary<Tile, Tile>
            Next = ImmutableDictionary.Of(BuildNext());

        public static Tile GetNext(Tile tile)
        {
            CheckArg.NotNull(tile, "tile");
            return Next[tile];
        }

        private static IDictionary<Tile, Tile> BuildNext()
        {
            var ranks = Enumerable.Range(1, 9);

            Tile[][] groups = {
                ranks.Select(r => Tile.Of(Suit.Dots , r)).ToArray(),
                ranks.Select(r => Tile.Of(Suit.Bams , r)).ToArray(),
                ranks.Select(r => Tile.Of(Suit.Craks, r)).ToArray(),
                new [] { Tile.FE, Tile.FS, Tile.FW, Tile.FN },
                new [] { Tile.JP, Tile.JF, Tile.JC },
            };

            var map = new Dictionary<Tile, Tile>();
            for (int i = 0; i < groups.Length; i++) {
                Tile[] group = groups[i];
                for (int j = 0; j < group.Length; j++) {
                    map.Add(group[j], group[(j + 1) % group.Length]);
                }
            }
            return map;
        }
    }
}
