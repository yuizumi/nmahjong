using NUnit.Framework;
using NSubstitute;
using System;
using System.Collections.Generic;
using NMahjong.Testing;

namespace NMahjong.Aux
{
    [TestFixture]
    public class ReadOnlyDictionaryViewTest : TestHelper
    {
        private IDictionary<String, Int32> mView;

        private static KeyValuePair<String, Int32> KV(string key, int value)
        {
            return new KeyValuePair<String, Int32>(key, value);
        }

        [SetUp]
        public void Setup()
        {
            var dictionary = new Dictionary<String, Int32>() {
                {"foo", 10}, {"bar", 42},
            };
            mView = new ReadOnlyDictionaryView<String, Int32>(dictionary);
        }

        [Test]
        public void TestItemGet()
        {
            Assert.AreEqual(10, mView["foo"]);
            Assert.AreEqual(42, mView["bar"]);
            Assert.Throws<KeyNotFoundException>(() => { var _ = mView["xxx"]; });
            Assert.Throws<ArgumentNullException>(() => { var _ = mView[null]; });
        }

        [Test]
        public void TestItemSet()
        {
            Assert.Throws<NotSupportedException>(() => { mView["foo"] = 99; });
            Assert.Throws<NotSupportedException>(() => { mView["xxx"] = 99; });
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
        public void TestKeys()
        {
            CollectionAssert.AreEquivalent(new [] { "foo", "bar" }, mView.Keys);
        }

        [Test]
        public void TestValues()
        {
            CollectionAssert.AreEquivalent(new [] { 10, 42 }, mView.Values);
        }

        [Test]
        public void TestAddAsICollection()
        {
            Assert.Throws<NotSupportedException>(() => mView.Add(KV("foo", 99)));
            Assert.Throws<NotSupportedException>(() => mView.Add(KV("xxx", 99)));
        }

        [Test]
        public void TestAddAsIDictionary()
        {
            Assert.Throws<NotSupportedException>(() => mView.Add("foo", 99));
            Assert.Throws<NotSupportedException>(() => mView.Add("xxx", 99));
        }

        [Test]
        public void TestClear()
        {
            Assert.Throws<NotSupportedException>(() => mView.Clear());
        }

        [Test]
        public void TestContains()
        {
            Assert.IsTrue(mView.Contains(KV("foo", 10)));
            Assert.IsTrue(mView.Contains(KV("bar", 42)));
            Assert.IsFalse(mView.Contains(KV("xxx", 99)));
            Assert.IsFalse(mView.Contains(KV("foo", 42)));
        }

        [Test]
        public void TestContainsKey()
        {
            Assert.IsTrue(mView.ContainsKey("foo"));
            Assert.IsTrue(mView.ContainsKey("bar"));
            Assert.IsFalse(mView.ContainsKey("xxx"));
        }

        [Test]
        public void TestCopyTo()
        {
            KeyValuePair<String, Int32>[] array = {
                KV("aaa", -1), KV("bbb", -2), KV("ccc", -3), KV("ddd", -4), KV("eee", -5)
            };
            mView.CopyTo(array, 2);
            // Unmodified elements.
            Assert.AreEqual(KV("aaa", -1), array[0]);
            Assert.AreEqual(KV("bbb", -2), array[1]);
            Assert.AreEqual(KV("eee", -5), array[4]);
            // Modified elements.
            CollectionAssert.AreEquivalent(
                new [] { KV("foo", 10), KV("bar", 42) }, new [] { array[2], array[3] });
        }

        [Test]
        public void TestGetEnumerator()
        {
            CollectionAssert.AreEquivalent(
                new [] { KV("foo", 10), KV("bar", 42) }, Enumerate(mView.GetEnumerator()));
        }

        [Test]
        public void TestRemoveAsICollection()
        {
            Assert.Throws<NotSupportedException>(() => mView.Remove(KV("foo", 10)));
            Assert.Throws<NotSupportedException>(() => mView.Remove(KV("xxx", 99)));
        }

        [Test]
        public void TestRemoveAsIDictionary()
        {
            Assert.Throws<NotSupportedException>(() => mView.Remove("foo"));
            Assert.Throws<NotSupportedException>(() => mView.Remove("xxx"));
        }

        [Test]
        public void TestTryGetValue()
        {
            int foo = -1, xxx = -1;
            Assert.IsTrue(mView.TryGetValue("foo", out foo));
            Assert.AreEqual(10, foo);
            Assert.IsFalse(mView.TryGetValue("xxx", out xxx));
            Assert.AreEqual(0, xxx);  // default(int).
        }
    }
}
