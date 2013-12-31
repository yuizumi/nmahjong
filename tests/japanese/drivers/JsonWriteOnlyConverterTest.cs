using NUnit.Framework;
using System;
using Newtonsoft.Json;

namespace NMahjong.Japanese.Drivers
{
    [TestFixture]
    public class JsonWriteOnlyConverterTest
    {
        //----------------------------------------------------------------------

        private class Foo
        {
            public Foo(object value)
            {
                Value = value;
            }

            public object Value
            {
                get; private set;
            }
        }

        private class FooPlus : Foo
        {
            public FooPlus(object value) : base(value)
            {
            }
        }

        private class FooConverter : JsonWriteOnlyConverter<Foo>
        {
            protected override object Convert(Foo value)
            {
                return value.Value;
            }
        }

        //----------------------------------------------------------------------

        private FooConverter mConverter;

        [SetUp]
        public void Setup()
        {
            mConverter = new FooConverter();
        }

        private string Serialize(Foo foo)
        {
            return JsonConvert.SerializeObject(foo, mConverter);
        }

        [Test]
        public void TestCanConvert()
        {
            Assert.IsTrue(mConverter.CanConvert(typeof(Foo)));
            // A derived class.
            Assert.IsTrue(mConverter.CanConvert(typeof(FooPlus)));
            // A base class.
            Assert.IsFalse(mConverter.CanConvert(typeof(Object)));
            // An unrelated class.
            Assert.IsFalse(mConverter.CanConvert(typeof(String)));
        }

        [Test]
        public void TestSerialize()
        {
            Assert.AreEqual(@"""hello""", Serialize(new Foo("hello")));
            Assert.AreEqual("0", Serialize(new Foo(0)));
            Assert.AreEqual(@"{""foo"":42,""bar"":""hello, world.""}",
                            Serialize(new Foo(new { foo = 42, bar = "hello, world." })));
        }

        //----------------------------------------------------------------------
    }
}
