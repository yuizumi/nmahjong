using NUnit.Framework;
using NMahjong.Base;
using NMahjong.Testing;

namespace NMahjong.Japanese
{
    [TestFixture]
    public class DoraHelperTest : TestHelperWithTiles
    {
        [Test, TestCaseSource("TestGetNextSource")]
        public void TestGetNext(Tile tile, Tile expected)
        {
            Assert.AreEqual(expected, DoraHelper.GetNext(tile));
        }

        #pragma warning disable 414
        private static readonly TestCaseData[] 
        TestGetNextSource = {
            Data(T1, T2), Data(T2, T3), Data(T3, T4),
            Data(T4, T5), Data(T5, T6), Data(T6, T7),
            Data(T7, T8), Data(T8, T9), Data(T9, T1),
            Data(S1, S2), Data(S2, S3), Data(S3, S4),
            Data(S4, S5), Data(S5, S6), Data(S6, S7),
            Data(S7, S8), Data(S8, S9), Data(S9, S1),
            Data(W1, W2), Data(W2, W3), Data(W3, W4),
            Data(W4, W5), Data(W5, W6), Data(W6, W7),
            Data(W7, W8), Data(W8, W9), Data(W9, W1),
            Data(FE, FS), Data(FS, FW), Data(FW, FN), Data(FN, FE),
            Data(JP, JF), Data(JF, JC), Data(JC, JP),
        };
        #pragma warning restore 414
    }
}
