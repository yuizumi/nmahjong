using NUnit.Framework;
using NMahjong.Testing;

using MS = NMahjong.Base.MeldState;

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
            Data(Chow.Of(T5, MS.Concealed), false), Data(Chow.Of(T5, MS.Open), true),
            Data(Pung.Of(T5, MS.Concealed), false), Data(Pung.Of(T5, MS.Open), true),
            Data(Kong.Of(T5, MS.Concealed), false), Data(Kong.Of(T5, MS.Open), true),
        };
        #pragma warning restore 414
    }
}
