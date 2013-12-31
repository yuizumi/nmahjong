using NUnit.Framework;
using NMahjong.Testing;

namespace NMahjong.Base
{
    [TestFixture]
    public class MeldsTest : TestHelperWithTiles
    {
        [Test, TestCaseSource("TestIsOpenSource")]
        public void TestIsOpen(Meld meld, bool expected)
        {
            Assert.AreEqual(expected, meld.IsOpen());
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestIsOpenSource = {
            Data(Chow.Concealed(T5), false), Data(Chow.Open(T5), true),
            Data(Pung.Concealed(T5), false), Data(Pung.Open(T5), true),
            Data(Kong.Concealed(T5), false), Data(Kong.Open(T5), true),
        };
        #pragma warning restore 414
    }
}
