using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NMahjong.Aux
{
    [TestFixture]
    public class ImmutableListTest
    {
        [Test]
        public void TestOfEmpty()
        {
            Assert.AreSame(ImmutableList<Int32>.Empty, ImmutableList.Of<Int32>());
        }

        [Test]
        public void TestOfParams()
        {
            Assert.AreEqual(new [] { 10, 15, 99 }, ImmutableList.Of(10, 15, 99));
        }

        [Test]
        public void TestOfNonEmptyArray()
        {
            int[] array = { 10, 15, 99 };
            var list = ImmutableList.Of(array);
            Assert.AreEqual(new [] { 10, 15, 99 }, list);
            // Change to array is not reflected.
            array[0] = -1;
            Assert.AreEqual(new [] { 10, 15, 99 }, list);
        }

        [Test]
        public void TestOfEmptyArray()
        {
            Assert.AreSame(ImmutableList<Int32>.Empty, ImmutableList.Of(new int[0]));
        }

        [Test]
        public void TestOfNonEmptyEnumerable()
        {
            IEnumerable<Int32> enumerable = Enumerable.Range(10, 4);
            Assert.AreEqual(new [] { 10, 11, 12, 13 }, ImmutableList.Of(enumerable));
        }

        [Test]
        public void TestOfEmptyEnumerable()
        {
            IEnumerable<Int32> enumerable = Enumerable.Empty<Int32>();
            Assert.AreSame(ImmutableList<Int32>.Empty, ImmutableList.Of(enumerable));
        }
    }
}
