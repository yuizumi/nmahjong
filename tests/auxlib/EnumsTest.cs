using NUnit.Framework;
using NMahjong.Testing;

namespace NMahjong.Aux
{
    [TestFixture]
    public class EnumsTest : TestHelper
    {
        [VisibleForTesting]
        public enum SomeEnum { Foo, Bar, Baz }

        [Test, TestCaseSource("TestIsValidSource")]
        public void TestIsValid(SomeEnum value, bool expected)
        {
            Assert.AreEqual(expected, Enums.IsValid(value));
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestIsValidSource = {
            Data(SomeEnum.Foo, true), Data(SomeEnum.Bar, true),
            Data(SomeEnum.Baz, true), Data((SomeEnum)(-1), false),
        };
        #pragma warning restore 414

        [Test]
        public void TestValues()
        {
            Assert.AreEqual(new [] { SomeEnum.Foo, SomeEnum.Bar, SomeEnum.Baz },
                            Enums.Values<SomeEnum>());
        }
    }
}
