using NUnit.Framework;
using System;
using NMahjong.Testing;

namespace NMahjong.Aux
{
    [TestFixture]
    public class CheckStateTest : TestHelper
    {
        [Test]
        public void TestExpect2NoThrow()
        {
            Assert.DoesNotThrow(() => CheckState.Expect(true, "dead"));
        }

        [Test]
        public void TestExpect2Throw()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(
                () => CheckState.Expect(false, "dead"));
            Assert.AreEqual("dead", ex.Message);
        }

        [Test]
        public void TestExpect3NoThrow()
        {
            Assert.DoesNotThrow(() => CheckState.Expect(true, "{0:x}", 57005));
        }

        [Test]
        public void TestExpect3Throw()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(
                () => CheckState.Expect(false, "{0:x}", 57005));
            Assert.AreEqual("dead", ex.Message);
        }

        [Test]
        public void TestIsSetClassNoThrow()
        {
            string foo = "";
            Assert.DoesNotThrow(() => CheckState.IsSet(foo, "Foo"));
        }

        [Test]
        public void TestIsSetClassThrow()
        {
            string foo = null;
            Assert.Throws<InvalidOperationException>(() => CheckState.IsSet(foo, "Foo"));
        }

        [Test]
        public void TestIsSetNullableNoThrow()
        {
            int? foo = 0;
            Assert.DoesNotThrow(() => CheckState.IsSet(foo, "Foo"));
        }

        [Test]
        public void TestIsSetNullableThrow()
        {
            int? foo = null;
            Assert.Throws<InvalidOperationException>(() => CheckState.IsSet(foo, "Foo"));
        }
    }
}
