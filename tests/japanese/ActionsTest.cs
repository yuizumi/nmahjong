using NUnit.Framework;
using NMahjong.Testing;

namespace NMahjong.Japanese
{
    [TestFixture]
    public class ActionsTest : TestHelperWithRevealedMelds
    {
        [Test, TestCaseSource("TestAreIdenticalSource")]
        public void TestAreIdentical(IPlayerAction a, IPlayerAction b, bool expected)
        {
            Assert.AreEqual(expected, Actions.AreIdentical(a, b));
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestAreIdenticalSource = {
            Data(Actions.AbortiveDraw(), Actions.AbortiveDraw(), true),
            Data(Actions.Chow(OChow(T5r, T6p, T7p, Player3)),
                 Actions.Chow(OChow(T5r, T6p, T7p, Player3)), true),
            Data(Actions.ConcealedKong(CKong(T5r, T5p, T5p, T5p)),
                 Actions.ConcealedKong(CKong(T5r, T5p, T5p, T5p)), true),
            Data(Actions.Discard(T5p), Actions.Discard(T5p), true),
            Data(Actions.Discard(Drawn(T5r)), Actions.Discard(Drawn(T5r)), true),
            Data(Actions.ExtendedKong(EKong(T5r, T5p, T5p, Player2, T5p)),
                 Actions.ExtendedKong(EKong(T5r, T5p, T5p, Player2, T5p)), true),
            Data(Actions.Mahjong(), Actions.Mahjong(), true),
            Data(Actions.None(), Actions.None(), true),
            Data(Actions.OpenKong(OKong(T5r, T5p, T5p, T5p, Player2)),
                 Actions.OpenKong(OKong(T5r, T5p, T5p, T5p, Player2)), true),
            Data(Actions.Pung(OPung(T5r, T5p, T5p, Player2)),
                 Actions.Pung(OPung(T5r, T5p, T5p, Player2)), true),
            Data(Actions.Riichi(), Actions.Riichi(), true),
            // Different types.
            Data(Actions.Mahjong(), Actions.Riichi(), false),
            Data(Actions.Discard(T5p), Actions.Pung(OPung(T5r, T5p, T5p, Player2)), false),
            Data(Actions.Pung(OPung(T5r, T5p, T5p, Player2)), Actions.Discard(T5p), false),
            // Different melds.
            Data(Actions.Chow(OChow(T5r, T6p, T7p, Player3)),
                 Actions.Pung(OPung(T5r, T5p, T5p, Player2)), false),
            Data(Actions.Chow(OChow(T5r, T6p, T7p, Player3)),
                 Actions.Chow(OChow(T5r, T7p, T6p, Player3)), false),
            Data(Actions.Pung(OPung(T5r, T5p, T5p, Player2)),
                 Actions.Pung(OPung(T5r, T5p, T5p, Player3)), false),
            // Different tiles.
            Data(Actions.Discard(T5r), Actions.Discard(T5p), false),
            Data(Actions.Discard(T5r), Actions.Discard(Drawn(T5r)), false),
        };
        #pragma warning restore 414
    }
}
