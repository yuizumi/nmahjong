using System;
using System.Collections.Generic;
using System.Linq;
using NMahjong.Aux;
using NMahjong.Base;
using NMahjong.Japanese;

namespace NMahjong.Sample
{
    internal static class ShantenHelper
    {
        // NOTE(yuizumi): mid = Meld ID, tid = Tile ID.

        private static readonly ImmutableList<ImmutableList<Int32>>
            MidToTids = BuildMidToTids();

        private static readonly int NumTiles = Tile.AllTiles.Count;
        private static readonly int NumMelds = MidToTids.Count;

        private static ImmutableList<ImmutableList<Int32>> BuildMidToTids()
        {
            var map = new List<ImmutableList<Int32>>();
            map.AddRange(Pung.GetAllPungs(MeldState.Concealed).Select(
                             m => m.Tiles.Select(Tiles.GetIndex).ToImmutableList()));
            map.AddRange(Chow.GetAllChows(MeldState.Concealed).Select(
                             m => m.Tiles.Select(Tiles.GetIndex).ToImmutableList()));
            return map.ToImmutableList();
        }

        internal static int[] GetVector(IEnumerable<AnnotatedTile> tiles)
        {
            var vector = new int[NumTiles];
            foreach (AnnotatedTile tile in tiles) ++vector[tile.BaseTile.GetIndex()];
            return vector;
        }

        internal static int ComputeShantensu(int[] vector, int numMelds)
        {
            return ComputeShantensu(numMelds, vector, new int[NumTiles], 0, -1, 99);
        }

        private static int ComputeShantensu(int rest, int[] hand, int[] goal, int mid,
                                            int count, int limit)
        {
            if (rest == 0) {
                int left = Enumerable.Range(0, NumTiles).Max(i => hand[i] - goal[i]);
                limit = Math.Min(limit, count + 2 - Math.Min(left, 2));
            } else {
                for (; mid < NumMelds; ++mid) {
                    var tids = MidToTids[mid];
                    int newCount = count;
                    bool valid = true;
                    foreach (int tid in tids) {
                        if (++goal[tid] > hand[tid]) ++newCount;
                        valid = valid && (goal[tid] <= 4);
                    }
                    if (valid && newCount < limit) {
                        limit = ComputeShantensu(rest - 1, hand, goal, mid, newCount, limit);
                    }
                    foreach (int tid in tids) --goal[tid];
                }
            }

            return limit;
        }
    }
}
