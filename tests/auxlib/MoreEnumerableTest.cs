using NUnit.Framework;
using System;
using System.Collections.Generic;
using NMahjong.Testing;

namespace NMahjong.Aux
{
    [TestFixture]
    public class MoreEnumerableTest : TestHelper
    {
        [Test]
        public void TestAppend()
        {
            string[] source = { "foo", "bar" };
            Assert.AreEqual(new [] { "foo", "bar", "baz" }, source.Append("baz"));
        }

        [Test, TestCaseSource("TestBracedStringSource")]
        public void TestBracedString(IEnumerable<String> source, string expected)
        {
            Assert.AreEqual(expected, source.BracedString());
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestBracedStringSource = {
            Data(List<String>(), "{}"),
            Data(List<String>("foo"), "{foo}"),
            Data(List<String>("foo", "bar", "baz", "qux"), "{foo, bar, baz, qux}"),
            Data(List<String>("", "foo"), "{, foo}"),
        };
        #pragma warning restore 414

        [Test]
        public void TestForEach()
        {
            string[] source = { "foo", "bar", "baz" };
            var list = new List<String>();
            source.ForEach(list.Add);
            Assert.AreEqual(new [] { "foo", "bar", "baz" }, list);
        }

        [Test, TestCaseSource("TestIsEmptySource")]
        public void TestIsEmpty(IEnumerable<String> source, bool expected)
        {
            Assert.AreEqual(expected, source.IsEmpty());
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestIsEmptySource = {
            Data(List<String>(), true),
            Data(List<String>("foo"), false),
            Data(List<String>("foo", "bar", "baz"), false),
        };
        #pragma warning restore 414
    }
}
