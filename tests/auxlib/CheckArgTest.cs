using NUnit.Framework;
using System;
using NMahjong.Testing;

namespace NMahjong.Aux
{
    [TestFixture]
    public class CheckArgTest : TestHelper
    {
        private enum FooEnum { A, B, C }

        [Test]
        public void TestExpect2NoThrow()
        {
            Assert.DoesNotThrow(() => CheckArg.Expect(true, "dead"));
        }

        [Test]
        public void TestExpect2Throw()
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(
                () => CheckArg.Expect(false, "dead"));
            Assert.IsNull(ex.ParamName);
            Assert.AreEqual("dead", ex.Message);
        }

        [Test]
        public void TestExpect3NoThrow()
        {
            Assert.DoesNotThrow(() => CheckArg.Expect(true, "foo", "dead"));
        }

        [Test]
        public void TestExpect3Throw()
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(
                () => CheckArg.Expect(false, "foo", "dead"));
            Assert.AreEqual("foo", ex.ParamName);
            StringAssert.StartsWith("dead", ex.Message);
        }

        [Test]
        public void TestExpect4NoThrow()
        {
            Assert.DoesNotThrow(() => CheckArg.Expect(true, "foo", "{0:x}", 0xdead));
        }

        [Test]
        public void TestExpect4Throw()
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(
                () => CheckArg.Expect(false, "foo", "{0:x}", 0xdead));
            Assert.AreEqual("foo", ex.ParamName);
            StringAssert.StartsWith("dead", ex.Message);
        }

        [Test]
        public void TestNotNullNoThrow()
        {
            string foo = "";
            Assert.DoesNotThrow(() => CheckArg.NotNull(foo, "foo"));
        }

        [Test]
        public void TestNotNullThrow()
        {
            string foo = null;
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(
                () => CheckArg.NotNull(foo, "foo"));
            Assert.AreEqual("foo", ex.ParamName);
        }

        [Test]
        public void TestEnumNoThrow()
        {
            FooEnum foo = FooEnum.B;
            Assert.DoesNotThrow(() => CheckArg.Enum(foo, "foo"));
        }

        [Test]
        public void TestEnumThrow()
        {
            FooEnum foo = (FooEnum)(-1);
            ArgumentException ex = Assert.Throws<ArgumentException>(
                () => CheckArg.Enum(foo, "foo"));
            Assert.AreEqual("foo", ex.ParamName);
        }

        [Test]
        public void TestMinimumCondition()
        {
            Action<Int32> tester = x => CheckArg.Minimum(x, "x", 50);
            Assert.DoesNotThrow(() => tester(99));
            Assert.DoesNotThrow(() => tester(51));
            Assert.DoesNotThrow(() => tester(50));
            Assert.Throws<ArgumentOutOfRangeException>(() => tester(49));
            Assert.Throws<ArgumentOutOfRangeException>(() => tester(10));
        }

        [Test]
        public void TestMinimumException()
        {
            int foo = -1;
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(
                () => CheckArg.Minimum(foo, "foo", 0));
            Assert.AreEqual("foo", ex.ParamName);
        }

        [Test]
        public void TestMaximumCondition()
        {
            Action<Int32> tester = x => CheckArg.Maximum(x, "x", 50);
            Assert.DoesNotThrow(() => tester(10));
            Assert.DoesNotThrow(() => tester(49));
            Assert.DoesNotThrow(() => tester(50));
            Assert.Throws<ArgumentOutOfRangeException>(() => tester(51));
            Assert.Throws<ArgumentOutOfRangeException>(() => tester(99));
        }

        [Test]
        public void TestMaximumException()
        {
            int foo = 1;
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(
                () => CheckArg.Maximum(foo, "foo", 0));
            Assert.AreEqual("foo", ex.ParamName);
        }

        [Test]
        public void TestRangeCondition()
        {
            Action<Int32> tester = x => CheckArg.Range(x, "x", 40, 60);
            Assert.Throws<ArgumentOutOfRangeException>(() => tester(10));
            Assert.Throws<ArgumentOutOfRangeException>(() => tester(39));
            Assert.DoesNotThrow(() => tester(40));
            Assert.DoesNotThrow(() => tester(41));
            Assert.DoesNotThrow(() => tester(50));
            Assert.DoesNotThrow(() => tester(49));
            Assert.DoesNotThrow(() => tester(60));
            Assert.Throws<ArgumentOutOfRangeException>(() => tester(61));
            Assert.Throws<ArgumentOutOfRangeException>(() => tester(99));
        }

        [Test]
        public void TestRangeException()
        {
            int foo = 32768;
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(
                () => CheckArg.Range(foo, "foo", -32768, 32767));
            Assert.AreEqual("foo", ex.ParamName);
        }

        [Test]
        public void TestNotContainsNullNoThrow()
        {
            string[] foo = { "a1", "b2" };
            Assert.DoesNotThrow(() => CheckArg.NotContainsNull(foo, "foo"));
        }

        [Test]
        public void TestNotContainsNullThrow()
        {
            string[] foo = { "a1", null };
            ArgumentException ex = Assert.Throws<ArgumentException>(
                () => CheckArg.NotContainsNull(foo, "foo"));
            Assert.AreEqual("foo", ex.ParamName);
        }

        [Test]
        public void TestNotEmptyNoThrow()
        {
            string[] foo = new string[2];
            Assert.DoesNotThrow(() => CheckArg.NotEmpty(foo, "foo"));
        }

        [Test]
        public void TestNotEmptyThrow()
        {
            string[] foo = new string[0];
            ArgumentException ex = Assert.Throws<ArgumentException>(
                () => CheckArg.NotEmpty(foo, "foo"));
            Assert.AreEqual("foo", ex.ParamName);
        }
    }
}
