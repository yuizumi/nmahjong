using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using NMahjong.Testing;

namespace NMahjong.Base
{
    [TestFixture]
    public class QuadTest : TestHelper
    {
        private Quad<Int32> mQuad;

        [SetUp]
        public void Setup()
        {
            mQuad = Quad<Int32>.Create(10, 20, 30, 99);
        }

        [Test]
        public void TestItemInt32()
        {
            Assert.AreEqual(10, mQuad[0]);
            Assert.AreEqual(20, mQuad[1]);
            Assert.AreEqual(30, mQuad[2]);
            Assert.AreEqual(99, mQuad[3]);
            Assert.Throws<ArgumentOutOfRangeException>(() => { var _ = mQuad[-1]; });
            Assert.Throws<ArgumentOutOfRangeException>(() => { var _ = mQuad[4]; });
        }

        [Test]
        public void TestItemGetPlayerId()
        {
            Assert.AreEqual(10, mQuad[new PlayerId(0)]);
            Assert.AreEqual(20, mQuad[new PlayerId(1)]);
            Assert.AreEqual(30, mQuad[new PlayerId(2)]);
            Assert.AreEqual(99, mQuad[new PlayerId(3)]);
        }

        [Test]
        public void TestGetEnumerator()
        {
            Assert.AreEqual(new [] { 10, 20, 30, 99 }, Enumerate(mQuad.GetEnumerator()));
        }
    }

    [TestFixture]
    public class QuadAsIListTest : TestHelper
    {
        private IList<Int32> mList;

        [SetUp]
        public void Setup()
        {
            mList = Quad<Int32>.Create(10, 20, 30, 99);
        }

        [Test]
        public void TestItemGet()
        {
            Assert.AreEqual(10, mList[0]);
            Assert.AreEqual(20, mList[1]);
            Assert.AreEqual(30, mList[2]);
            Assert.AreEqual(99, mList[3]);
            Assert.Throws<ArgumentOutOfRangeException>(() => { var _ = mList[-1]; });
            Assert.Throws<ArgumentOutOfRangeException>(() => { var _ = mList[4]; });
        }

        [Test]
        public void TestItemSet()
        {
            Assert.Throws<NotSupportedException>(() => { mList[1] = 50; });
        }

        [Test]
        public void TestCount()
        {
            Assert.AreEqual(4, mList.Count);
        }

        [Test]
        public void TestIsReadOnly()
        {
            Assert.IsTrue(mList.IsReadOnly);
        }

        [Test]
        public void TestAdd()
        {
            Assert.Throws<NotSupportedException>(() => mList.Add(50));
        }

        [Test]
        public void TestClear()
        {
            Assert.Throws<NotSupportedException>(() => mList.Clear());
        }

        [Test]
        public void TestContains()
        {
            Assert.IsTrue(mList.Contains(10));
            Assert.IsTrue(mList.Contains(20));
            Assert.IsTrue(mList.Contains(30));
            Assert.IsTrue(mList.Contains(99));
            Assert.IsFalse(mList.Contains(50));
        }

        [Test]
        public void TestCopyTo()
        {
            int[] array = { -1, -2, -3, -4, -5, -6, -7 };
            mList.CopyTo(array, 2);
            Assert.AreEqual(new [] { -1, -2, 10, 20, 30, 99, -7 }, array);
        }

        [Test, TestCaseSource("TestCopyToInvalidSource")]
        public void TestCopyToInvalid(int[] array, int index, Type exceptionType)
        {
            Assert.Throws(exceptionType, () => mList.CopyTo(array, index));
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestCopyToInvalidSource = {
            Data(new int[2], 0, typeof(ArgumentException)),
            Data(new int[7], 4, typeof(ArgumentException)),
            Data(null, 2, typeof(ArgumentNullException)),
            Data(new int[7], -1, typeof(ArgumentOutOfRangeException)),
        };
        #pragma warning restore 414

        [Test]
        public void TestInsert()
        {
            Assert.Throws<NotSupportedException>(() => mList.Insert(0, 50));
            Assert.Throws<NotSupportedException>(() => mList.Insert(-1, 50));
        }

        [Test]
        public void TestRemove()
        {
            Assert.Throws<NotSupportedException>(() => mList.Remove(10));
            Assert.Throws<NotSupportedException>(() => mList.Remove(50));
        }

        [Test]
        public void TestRemoveAt()
        {
            Assert.Throws<NotSupportedException>(() => mList.Remove(0));
            Assert.Throws<NotSupportedException>(() => mList.Remove(-1));
        }
    }

    [TestFixture]
    public class QuadCreateTest : TestHelper
    {
        [Test]
        public void TestOfParams()
        {
            Quad<Int32> q = Quad.Of(10, 20, 30, 99);
            Assert.AreEqual(new [] { 10, 20, 30, 99 }, q);
        }

        [Test]
        public void TestOfEnumerable()
        {
            int[] source = { 10, 20, 30, 99 };
            Quad<Int32> q = Quad.Of(source);
            Assert.AreEqual(new [] { 10, 20, 30, 99 }, q);
            source[0] = 50;
            Assert.AreEqual(new [] { 10, 20, 30, 99 }, q);
        }

        [Test, TestCaseSource("TestOfEnumerableInvalidSource")]
        public void TestOfEnumerableInvalid(IList<Int32> source)
        {
            Assert.Throws<ArgumentException>(() => Quad.Of(source));
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestOfEnumerableInvalidSource = {
            Data(List(10, 20, 99)),
            Data(List(10, 20, 30, 40, 99)),
        };
        #pragma warning restore 414

        [Test]
        public void TestOfEnumerableAndOffset()
        {
            int[] source = { 10, 20, 30, 99 };
            Quad<Int32> q = Quad.Of(source, 3);
            Assert.AreEqual(new [] { 99, 10, 20, 30 }, q);
        }

        [Test, TestCaseSource("TestOfEnumerableAndOffsetInvalidSource")]
        public void TestOfEnumerableAndOffsetInvalid(IList<Int32> source, int offset,
                                                     Type exceptionType)
        {
            Assert.Throws(exceptionType, () => Quad.Of(source, offset));
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestOfEnumerableAndOffsetInvalidSource = {
            Data(List(10, 20, 30, 40, 99), 1, typeof(ArgumentException)),
            Data(List(10, 20, 99), 1, typeof(ArgumentException)),
            Data(List(10, 20, 30, 99), -9, typeof(ArgumentOutOfRangeException)),
            Data(List(10, 20, 30, 99), -1, typeof(ArgumentOutOfRangeException)),
            Data(List(10, 20, 30, 99),  4, typeof(ArgumentOutOfRangeException)),
            Data(List(10, 20, 30, 99), 10, typeof(ArgumentOutOfRangeException)),
        };
        #pragma warning restore 414

        [Test]
        public void TestOfGenerator()
        {
            Quad<Int32> q = Quad.Of(j => j * j);
            Assert.AreEqual(new [] { 0, 1, 4, 9 }, q);
        }
    }
}
