using NUnit.Framework;
using NMahjong.Testing;

namespace NMahjong.Base
{
    [TestFixture]
    public class PlayerIdTest : TestHelper
    {
        [Test, TestCaseSource("TestEqualitySource")]
        public void TestEquality(PlayerId x, PlayerId y, bool equal)
        {
            Assert.AreEqual(equal, x == y);
        }

        [Test, TestCaseSource("TestEqualitySource")]
        public void TestInequality(PlayerId x, PlayerId y, bool equal)
        {
            Assert.AreEqual(!equal, x != y);
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestEqualitySource = {
            Data(new PlayerId(0), new PlayerId(0), true),
            Data(new PlayerId(0), new PlayerId(1), false),
        };
        #pragma warning restore 414

        [Test]
        public void TestToString()
        {
            Assert.AreEqual("PlayerId(0)", new PlayerId(0).ToString());
        }
    }
}
