using NUnit.Framework;
using NMahjong.Testing;

namespace NMahjong.Base
{
    [TestFixture]
    public class TilesTest : TestHelperWithTiles
    {
        [Test, TestCaseSource("TestGetIndexSource")]
        public void TestGetIndex(Tile tile, int expected)
        {
            Assert.AreEqual(expected, tile.GetIndex());
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestGetIndexSource = {
            Data(T1, 0), Data(T2, 1), Data(T3, 2), Data(T4, 3), Data(T5, 4), Data(T6, 5),
            Data(T7, 6), Data(T8, 7), Data(T9, 8), Data(S1, 9), Data(S2,10), Data(S3,11),
            Data(S4,12), Data(S5,13), Data(S6,14), Data(S7,15), Data(S8,16), Data(S9,17),
            Data(W1,18), Data(W2,19), Data(W3,20), Data(W4,21), Data(W5,22), Data(W6,23),
            Data(W7,24), Data(W8,25), Data(W9,26), Data(FE,27), Data(FS,28), Data(FW,29),
            Data(FN,30), Data(JP,31), Data(JF,32), Data(JC,33),
        };
        #pragma warning restore 414

        [Test, TestCaseSource("TestIsNumberTileSource")]
        public void TestIsNumberTile(Tile tile, bool expected)
        {
            Assert.AreEqual(expected, tile.IsNumberTile());
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestIsNumberTileSource = {
            Data(T1, true), Data(T2, true), Data(T3, true),
            Data(T4, true), Data(T5, true), Data(T6, true),
            Data(T7, true), Data(T8, true), Data(T9, true),
            Data(S1, true), Data(S2, true), Data(S3, true),
            Data(S4, true), Data(S5, true), Data(S6, true),
            Data(S7, true), Data(S8, true), Data(S9, true),
            Data(W1, true), Data(W2, true), Data(W3, true),
            Data(W4, true), Data(W5, true), Data(W6, true),
            Data(W7, true), Data(W8, true), Data(W9, true),
            Data(FE, false), Data(FS, false), Data(FW, false), Data(FN, false),
            Data(JP, false), Data(JF, false), Data(JC, false),
        };
        #pragma warning restore 414
    }
}
