using NUnit.Framework;
using NSubstitute;
using System;
using System.Collections.Generic;
using NMahjong.Testing;

namespace NMahjong.Aux
{
    [TestFixture]
    public class ReadOnlyCollectionViewTest : TestHelper
    {
        private ICollection<Int32> mMock, mView;

        [SetUp]
        public void Setup()
        {
            mMock = Substitute.For<ICollection<Int32>>();
            mView = new ReadOnlyCollectionView<Int32>(mMock);
        }

        [Test]
        public void TestCount()
        {
            mMock.Count.Returns(3);
            Assert.AreEqual(3, mView.Count);
        }

        [Test]
        public void TestIsReadOnly()
        {
            mMock.IsReadOnly.Returns(false);
            Assert.IsTrue(mView.IsReadOnly);
        }

        [Test]
        public void TestAdd()
        {
            Assert.Throws<NotSupportedException>(() => mView.Add(99));
        }

        [Test]
        public void TestClear()
        {
            Assert.Throws<NotSupportedException>(() => mView.Clear());
        }

        [Test]
        public void TestContains()
        {
            mMock.Contains(10).Returns(true);
            Assert.IsTrue(mView.Contains(10));
            mMock.Contains(99).Returns(false);
            Assert.IsFalse(mView.Contains(99));
        }

        [Test]
        public void TestCopyTo()
        {
            var array = new int[0];
            mView.CopyTo(array, 1);
            mMock.ReceivedWithAnyArgs(1).CopyTo(null, 0);
            mMock.Received(1).CopyTo(array, 1);
        }

        [Test]
        public void TestGetEnumerator()
        {
            var enumerator = Substitute.For<IEnumerator<Int32>>();
            mMock.GetEnumerator().Returns(enumerator);
            Assert.AreEqual(enumerator, mView.GetEnumerator());
        }

        [Test]
        public void TestRemove()
        {
            Assert.Throws<NotSupportedException>(() => mView.Remove(99));
        }
    }
}
