using NUnit.Framework;
using System;
using NMahjong.Testing;

namespace NMahjong.Base
{
    [TestFixture]
    public class TileTest : TestHelperWithTiles
    {
        [Test, TestCaseSource("TestRankSource")]
        public void TestRank(Tile tile, int expected)
        {
            Assert.AreEqual(expected, tile.Rank);
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestRankSource = {
            Data(T1, 1), Data(T2, 2), Data(T3, 3), Data(T4, 4), Data(T5, 5),
            Data(T6, 6), Data(T7, 7), Data(T8, 8), Data(T9, 9),
            Data(S1, 1), Data(S2, 2), Data(S3, 3), Data(S4, 4), Data(S5, 5),
            Data(S6, 6), Data(S7, 7), Data(S8, 8), Data(S9, 9),
            Data(W1, 1), Data(W2, 2), Data(W3, 3), Data(W4, 4), Data(W5, 5),
            Data(W6, 6), Data(W7, 7), Data(W8, 8), Data(W9, 9),
        };
        #pragma warning restore 414

        [Test, TestCaseSource("TestRankInvalidSource")]
        public void TestRankInvalid(Tile tile)
        {
            Assert.Throws<InvalidOperationException>(
                () => { var _ = tile.Rank; });
        }

        #pragma warning disable 414
        private static readonly Tile[]
        TestRankInvalidSource = { FE, FS, FW, FN, JP, JF, JC };
        #pragma warning restore 414

        [Test, TestCaseSource("TestTileTypeSource")]
        public void TestTileType(Tile tile, TileType expected)
        {
            Assert.AreEqual(expected, tile.TileType);
        }

        private static TestCaseData[] TestTileTypeSource()
        {
            TileType s = TileType.Simple, t = TileType.Terminal,
                h = TileType.Honor;
            return new [] {
                Data(T1, t), Data(T2, s), Data(T3, s), Data(T4, s),
                Data(T5, s), Data(T6, s), Data(T7, s), Data(T8, s),
                Data(T9, t), Data(S1, t), Data(S2, s), Data(S3, s),
                Data(S4, s), Data(S5, s), Data(S6, s), Data(S7, s),
                Data(S8, s), Data(S9, t), Data(W1, t), Data(W2, s),
                Data(W3, s), Data(W4, s), Data(W5, s), Data(W6, s),
                Data(W7, s), Data(W8, s), Data(W9, t), Data(FE, h),
                Data(FS, h), Data(FW, h), Data(FN, h), Data(JP, h),
                Data(JF, h), Data(JC, h),
            };
        }

        [Test, TestCaseSource("TestOfSource")]
        public void TestOf(Suit suit, int rank, Tile expected)
        {
            Assert.AreEqual(expected, Tile.Of(suit, rank));
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestOfSource = {
            Data(Dots, 1, T1), Data(Dots, 2, T2), Data(Dots, 3, T3),
            Data(Dots, 4, T4), Data(Dots, 5, T5), Data(Dots, 6, T6),
            Data(Dots, 7, T7), Data(Dots, 8, T8), Data(Dots, 9, T9),
            Data(Bams, 1, S1), Data(Bams, 2, S2), Data(Bams, 3, S3),
            Data(Bams, 4, S4), Data(Bams, 5, S5), Data(Bams, 6, S6),
            Data(Bams, 7, S7), Data(Bams, 8, S8), Data(Bams, 9, S9),
            Data(Craks, 1, W1), Data(Craks, 2, W2), Data(Craks, 3, W3),
            Data(Craks, 4, W4), Data(Craks, 5, W5), Data(Craks, 6, W6),
            Data(Craks, 7, W7), Data(Craks, 8, W8), Data(Craks, 9, W9),
        };
        #pragma warning restore 414

        [Test, TestCaseSource("TestOfInvalidSource")]
        public void TestOfInvalid(Suit suit, int rank)
        {
            Tile.Of(suit, rank);  // throws Exception.
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestOfInvalidSource = {
            // Invalid suit.
            Data(Winds, 1).Throws(typeof(ArgumentException)),
            Data(Dragons, 1).Throws(typeof(ArgumentException)),
            // Invalid rank.
            Data(Dots, -99).Throws(typeof(ArgumentOutOfRangeException)),
            Data(Dots, 0).Throws(typeof(ArgumentOutOfRangeException)),
            Data(Dots, 10).Throws(typeof(ArgumentOutOfRangeException)),
            Data(Dots, 999).Throws(typeof(ArgumentOutOfRangeException)),
            // Invalid suit and rank.
            Data(Winds, 999).Throws(typeof(ArgumentException)),
            Data(Dragons, 999).Throws(typeof(ArgumentException)),
        };
        #pragma warning restore 414
    }
}
