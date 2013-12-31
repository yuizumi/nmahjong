using NUnit.Framework;
using System;
using System.Collections.Generic;
using Mono.Options;

namespace NMahjong.Aux.Tools
{
    [TestFixture]
    public class StrictOptionSetTest
    {
        private OptionSet mOptionSet;

        [SetUp]
        public void Setup()
        {
            mOptionSet = new StrictOptionSet() { {"foo:", (_ => {})} };
        }

        [Test]
        public void TestJustArguments()
        {
            List<String> args = mOptionSet.Parse(new [] { "x" });
            Assert.AreEqual(new [] { "x" }, args);
        }

        [Test]
        public void TestValidOption()
        {
            List<String> args = mOptionSet.Parse(new [] { "--foo=x" });
            Assert.AreEqual(new string[0], args);
        }

        [Test]
        public void TestInvalidOption()
        {
            var ex = Assert.Throws<OptionException>(() => mOptionSet.Parse(new [] { "--bar=x" }));
            Assert.AreEqual("Unknown option '--bar'.", ex.Message);
            Assert.AreEqual("--bar", ex.OptionName);
        }
    }
}
