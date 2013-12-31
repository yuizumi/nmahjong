using NUnit.Framework;
using NSubstitute;
using System;
using System.Collections.Generic;
using NMahjong.Testing;

namespace NMahjong.Aux
{
    [TestFixture]
    public class ReadOnlyListViewTest : TestHelper
    {
        private IList<Int32> mView;

        [SetUp]
        public void Setup()
        {
            mView = new ReadOnlyListView<Int32>(new List<Int32>() { 10, 42  });
        }

        [Test]
        public void TestItemGet()
        {
            Assert.AreEqual(10, mView[0]);
            Assert.AreEqual(42, mView[1]);
            Assert.Throws<ArgumentOutOfRangeException>(() => { var _ = mView[-1]; });
            Assert.Throws<ArgumentOutOfRangeException>(() => { var _ = mView[2]; });
        }

        [Test]
        public void TestItemSet()
        {
            Assert.Throws<NotSupportedException>(() => { mView[0] = 99; });
            Assert.Throws<NotSupportedException>(() => { mView[-1] = 99; });
        }

        [Test]
        public void TestCount()
        {
            Assert.AreEqual(2, mView.Count);
        }

        [Test]
        public void TestIsReadOnly()
        {
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
            Assert.IsTrue(mView.Contains(10));
            Assert.IsTrue(mView.Contains(42));
            Assert.IsFalse(mView.Contains(99));
        }

        [Test]
        public void TestCopyTo()
        {
            int[] array = { -1, -2, -3, -4, -5  };
            mView.CopyTo(array, 2);
            Assert.AreEqual(new [] { -1, -2, 10, 42, -5 }, array);
        }

        [Test]
        public void TestGetEnumerator()
        {
            Assert.AreEqual(new [] { 10, 42 }, Enumerate(mView.GetEnumerator()));
        }

        [Test]
        public void TestIndexOf()
        {
            Assert.AreEqual(0, mView.IndexOf(10));
            Assert.AreEqual(1, mView.IndexOf(42));
            Assert.AreEqual(-1, mView.IndexOf(99));
        }

        [Test]
        public void TestInsert()
        {
            Assert.Throws<NotSupportedException>(() => mView.Insert(0, 99));
            Assert.Throws<NotSupportedException>(() => mView.Insert(-1, 99));
        }

        [Test]
        public void TestRemove()
        {
            Assert.Throws<NotSupportedException>(() => mView.Remove(99));
        }

        [Test]
        public void TestRemoveAt()
        {
            Assert.Throws<NotSupportedException>(() => mView.RemoveAt(0));
            Assert.Throws<NotSupportedException>(() => mView.RemoveAt(-1));
        }
    }
}
