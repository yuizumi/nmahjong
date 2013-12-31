using NUnit.Framework;
using NSubstitute;
using System;

namespace NMahjong.Aux
{
    [TestFixture]
    public class DisposableContainerTest
    {
        [Test]
        public void TestAddAndDispose()
        {
            var container = new DisposableContainer();
            IDisposable obj1 = container.Add(Substitute.For<IDisposable>());
            IDisposable obj2 = container.Add(Substitute.For<IDisposable>());
            container.Dispose();
            obj1.Received(1).Dispose();
            obj2.Received(1).Dispose();
        }

        [Test]
        public void TestAddAfterDispose()
        {
            var container = new DisposableContainer();
            container.Dispose();
            Assert.Throws<ObjectDisposedException>(
                () => container.Add(Substitute.For<IDisposable>()));
        }
    }
}
