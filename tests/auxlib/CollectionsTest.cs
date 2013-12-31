using NUnit.Framework;
using System;
using System.Collections.Generic;
using NMahjong.Testing;

namespace NMahjong.Aux
{
    [TestFixture]
    public class CollectionsTest : TestHelper
    {
        [Test, TestCaseSource("TestAddRangeSource")]
        public void TestAddRange(IList<Int32> original, IList<Int32> items,
                                 IList<Int32> expected)
        {
            // Use LinkedList<T> to ensure we are not calling List<T>.AddRange().
            var list = new LinkedList<Int32>(original);
            list.AddRange(items);
            Assert.AreEqual(expected, list);
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestAddRangeSource = {
            Data(List(1, 2), List(3, 4), List(1, 2, 3, 4)),
            Data(List<Int32>(), List(1), List(1)),
            Data(List(1), List<Int32>(), List(1)),
            Data(List<Int32>(), List<Int32>(), List<Int32>()),
        };
        #pragma warning restore 414

        [Test, TestCaseSource("TestGet2Source")]
        public void TestGet2(IDictionary<String, Int32> dictionary,
                             string key, int expected)
        {
            Assert.AreEqual(expected, dictionary.Get(key));
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestGet2Source = {
            Data(new Dictionary<String, Int32>() {{"a", 10}, {"b", 20}}, "a", 10),
            Data(new Dictionary<String, Int32>() {{"a", 10}, {"b", 20}}, "x", 0),
        };
        #pragma warning restore 414

        [Test, TestCaseSource("TestGet3Source")]
        public void TestGet3(IDictionary<String, Int32> dictionary,
                             string key, int defaultValue, int expected)
        {
            Assert.AreEqual(expected, dictionary.Get(key, defaultValue));
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestGet3Source = {
            Data(new Dictionary<String, Int32>() {{"a", 10}, {"b", 20}}, "a", -1, 10),
            Data(new Dictionary<String, Int32>() {{"a", 10}, {"b", 20}}, "x", -1, -1),
        };
        #pragma warning restore 414
    }
}
