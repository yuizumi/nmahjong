using System.Linq;
using NUnit.Framework;
using NSubstitute;
using Mono.Options;

namespace NMahjong.Aux.Tools
{
    [TestFixture]
    public class ComplexOptionSetProviderTest
    {
        // Public is for NSubstitute.
        public abstract class FooOptions : OptionSetProvider {}
        public abstract class BarOptions : OptionSetProvider {}
        public abstract class BazOptions : OptionSetProvider {}

        private FooOptions mFooOptions;
        private BarOptions mBarOptions;
        private ComplexOptionSetProvider mProvider;

        [SetUp]
        public void Setup()
        {
            mProvider = new ComplexOptionSetProvider();
            mFooOptions = Substitute.For<FooOptions>();
            mProvider.Add(mFooOptions);
            mBarOptions = Substitute.For<BarOptions>();
            mProvider.Add(mBarOptions);
        }

        [Test]
        public void TestGet()
        {
            Assert.AreEqual(mFooOptions, mProvider.Get<FooOptions>());
            Assert.AreEqual(mBarOptions, mProvider.Get<BarOptions>());
            Assert.IsNull(mProvider.Get<BazOptions>());
        }

        [Test]
        public void TestGetOptionSet()
        {
            var foo = new OptionSet() { {"a", (_ => {})}, {"b", (_ => {})} };
            mFooOptions.GetOptionSet().Returns(foo);
            var bar = new OptionSet() { {"c", (_ => {})} };
            mBarOptions.GetOptionSet().Returns(bar);
            Assert.AreEqual(foo.Concat(bar), mProvider.GetOptionSet());
        }

        [Test]
        public void TestOnParseComplete()
        {
            mProvider.OnParseComplete();
            mFooOptions.Received(1).OnParseComplete();
            mBarOptions.Received(1).OnParseComplete();
        }
    }
}
