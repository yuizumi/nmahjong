using System;
using System.Collections.Generic;
using System.Linq;
using NMahjong.Base;
using NMahjong.Japanese;

using MS = NMahjong.Base.MeldState;

namespace NMahjong.Sample
{
    // NOTE(yuizumi): mid = Meld ID, tid = Tile ID.

    internal static class ShantenHelper
    {
        // Use arrays (instead of ImmutableLists) for 5x faster speed.
        private static readonly int[][] MidToTids = BuildMidToTids();

        private static readonly int NumTiles = Tile.AllTiles.Count;
        private static readonly int NumMelds = MidToTids.Length;

        private static int[][] BuildMidToTids()
        {
            var melds = Enumerable.Concat(Pung.GetAllPungs(MS.Concealed).Cast<Meld>(),
                                          Chow.GetAllChows(MS.Concealed).Cast<Meld>());
            return melds.Select(m => m.Tiles.Select(Tiles.GetIndex).ToArray()).ToArray();
        }

        internal static int[] GetVector(IEnumerable<AnnotatedTile> tiles)
        {
            var vector = new int[NumTiles];
            foreach (AnnotatedTile tile in tiles) ++vector[tile.BaseTile.GetIndex()];
            return vector;
        }

        internal static int GetShantensu(int[] vector, int reqMelds)
        {
            return ComputeShantensu(reqMelds, vector, new int[NumTiles], 0, -1, 99);
        }

        private static int ComputeShantensu(int reqMelds, int[] hand, int[] goal, int mid,
                                            int accum, int limit)
        {
            if (reqMelds == 0) {
                for (int tid = 0; tid < NumTiles; ++tid) {
                    if (hand[tid] - goal[tid] >= 2) {
                        limit = accum;
                        break;
                    }
                    if (goal[tid] <= 2) {
                        int newAccum = accum + Math.Min(goal[tid] + 2 - hand[tid], 2);
                        limit = Math.Min(limit, newAccum);
                    }
                }
            } else {
                for (; mid < NumMelds; ++mid) {
                    var tids = MidToTids[mid];
                    int newAccum = accum;
                    bool valid = true;
                    foreach (int tid in tids) {
                        ++goal[tid];
                        if (goal[tid] > hand[tid]) ++newAccum;
                        if (goal[tid] > 4) valid = false;
                    }
                    if (valid && newAccum < limit) {
                        limit = ComputeShantensu(reqMelds - 1, hand, goal, mid, newAccum, limit);
                    }
                    foreach (int tid in tids) --goal[tid];
                }
            }

            return limit;
        }
    }
}
