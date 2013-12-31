using NUnit.Framework;
using System;
using NMahjong.Testing;

namespace NMahjong.Japanese.Engine
{
    [TestFixture]
    public class CheckActionTest : TestHelper
    {
        [Test]
        public void TestExpect2NoThrow()
        {
            Assert.DoesNotThrow(() => CheckAction.Expect(true, "dead"));
        }

        [Test]
        public void TestExpect2Throw()
        {
            InvalidActionException ex = Assert.Throws<InvalidActionException>(
                () => CheckAction.Expect(false, "dead"));
            Assert.AreEqual("dead", ex.Message);
        }

        [Test]
        public void TestExpect3NoThrow()
        {
            Assert.DoesNotThrow(() => CheckAction.Expect(true, "{0:x}", 57005));
        }

        [Test]
        public void TestExpect3Throw()
        {
            InvalidActionException ex = Assert.Throws<InvalidActionException>(
                () => CheckAction.Expect(false, "{0:x}", 57005));
            Assert.AreEqual("dead", ex.Message);
        }

        [Test]
        public void TestNotNullNoThrow()
        {
            IPlayerAction action = Actions.None();
            Assert.DoesNotThrow(() => CheckAction.NotNull(action));
        }

        [Test]
        public void TestNotNullThrow()
        {
            IPlayerAction action = null;
            Assert.Throws<InvalidActionException>(() => CheckAction.NotNull(action));
        }
    }
}
