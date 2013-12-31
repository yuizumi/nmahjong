using NUnit.Framework;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NMahjong.Aux;

namespace NMahjong.Drivers.Mjai
{
    [TestFixture]
    public class DictionaryBasedConverterTest
    {
        private enum SomeEnum { Foo, Bar, Baz };

        private DictionaryBasedConverter<SomeEnum> mConverter;

        [SetUp]
        public void Setup()
        {
            var mapping = new Dictionary<String, SomeEnum>() {
                {"foo", SomeEnum.Foo},
                {"bar", SomeEnum.Bar},
                {"baz", SomeEnum.Baz},
            };
            mConverter = DictionaryBasedConverter.Create(ImmutableDictionary.Of(mapping));
        }

        private T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, mConverter);
        }

        private string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value, mConverter);
        }

        [Test]
        public void TestDeserialize()
        {
            Assert.AreEqual(SomeEnum.Bar, Deserialize<SomeEnum>(@"""bar"""));
        }

        [Test]
        public void TestDeserializeNonString()
        {
            // 1 == (int) SomeEnum.Bar.
            Assert.Throws<JsonSerializationException>(() => Deserialize<SomeEnum>("1"));
        }

        [Test]
        public void TestDeserializeUnknownString()
        {
            Assert.Throws<JsonSerializationException>(
                () => Deserialize<SomeEnum>(@"""nonexistent"""));
        }

        [Test]
        public void TestSerialize()
        {
            Assert.AreEqual(@"""bar""", Serialize(SomeEnum.Bar));
        }

        [Test]
        public void TestSerializeUnknownValue()
        {
            Assert.Throws<JsonSerializationException>(() => Serialize((SomeEnum)(-1)));
        }
    }
}
