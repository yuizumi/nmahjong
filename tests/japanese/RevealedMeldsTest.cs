using NUnit.Framework;
using NMahjong.Testing;

namespace NMahjong.Japanese
{
    [TestFixture]
    public class RevealedMeldsTest : TestHelperWithRevealedMelds
    {
        [Test, TestCaseSource("TestAreIdenticalSource")]
        public void TestAreIdentical(RevealedMeld a, RevealedMeld b, bool expected)
        {
            Assert.AreEqual(expected, RevealedMelds.AreIdentical(a, b));
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestAreIdenticalSource = {
            Data(OChow(T5r, T6p, T7p, Player1), OChow(T6p, T5r, T7p, Player1), true ),
            Data(OPung(T5r, T5p, T5p, Player1), OPung(T5p, T5r, T5p, Player1), true ),
            Data(OKong(T5r, T5p, T5p, T5p, Player1), OKong(T5p, T5p, T5r, T5p, Player1), true ),
            Data(EKong(T5r, T5p, T5p, Player1, T5p), EKong(T5p, T5r, T5p, Player1, T5p), true ),
            Data(CKong(T5r, T5p, T5p, T5p), CKong(T5p, T5p, T5p, T5r), true ),
            // Different types.
            Data(OPung(T5r, T5p, T5p, Player1), OChow(T5r, T6p, T7p, Player1), false),
            Data(OPung(T5r, T5p, T5p, Player1), OKong(T5r, T5p, T5p, T5p, Player1), false),
            // Different tiles.
            Data(OPung(T5r, T5p, T5p, Player1), OPung(S5r, S5p, S5p, Player1), false),
            Data(OPung(T5r, T5p, T5p, Player1), OPung(T5p, T5p, T5p, Player1), false),
            Data(OPung(T5r, T5p, T5p, Player1), OPung(T5p, T5p, T5r, Player1), false),
            Data(EKong(T5r, T5p, T5p, Player1, T5p), EKong(T5p, T5p, T5r, Player1, T5p), false),
            Data(EKong(T5r, T5p, T5p, Player1, T5p), EKong(T5p, T5p, T5p, Player1, T5r), false),
            // Different feeders.
            Data(OPung(T5r, T5p, T5p, Player1), OPung(T5r, T5p, T5p, Player2), false),
            Data(EKong(T5r, T5p, T5p, Player1, T5p), EKong(T5r, T5p, T5p, Player2, T5p), false),
        };
        #pragma warning restore 414
    }
}
