using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NMahjong.Aux
{
    [TestFixture]
    public class ImmutableDictionaryTest
    {
        private static readonly ImmutableDictionary<String, Int32> Empty =
            ImmutableDictionary<String, Int32>.Empty;

        private static KeyValuePair<String, Int32> KV(string key, int value)
        {
            return new KeyValuePair<String, Int32>(key, value);
        }

        [Test]
        public void TestOfEmpty()
        {
            Assert.AreSame(Empty, ImmutableDictionary.Of<String, Int32>());
        }

        [Test]
        public void TestOfNonEmptyDictionary()
        {
            var wrappee = new Dictionary<String, Int32>() { {"foo", 10}, {"bar", 15} };
            var view = ImmutableDictionary.Of(wrappee);
            CollectionAssert.AreEquivalent(new [] { KV("foo", 10), KV("bar", 15) }, view);
            // Change to array is not reflected.
            wrappee.Add("xxx", 42);
            CollectionAssert.AreEquivalent(new [] { KV("foo", 10), KV("bar", 15) }, view);
        }

        [Test]
        public void TestOfEmptyDictionary()
        {
            Assert.AreSame(ImmutableDictionary<String, Int32>.Empty,
                           ImmutableDictionary.Of(new Dictionary<String, Int32>()));
        }
    }
}
