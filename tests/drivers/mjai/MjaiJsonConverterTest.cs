using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NMahjong.Drivers.Mjai
{
    [TestFixture]
    public class MjaiJsonConverterTest
    {
        private MjaiJson Deserialize(string value)
        {
            return JsonConvert.DeserializeObject<MjaiJson>(value, MjaiJsonConverter.Converter);
        }

        private string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value, MjaiJsonConverter.Converter);
        }

        [Test]
        public void TestDeserialize()
        {
            MjaiJson mjson = Deserialize(@"{""type"":""none""}");
            Assert.AreEqual("none", mjson.Type);
        }

        [Test]
        public void TestDeserializeIncompleteObject()
        {
            var ex = Assert.Throws<MjaiMissingFieldException>(() => Deserialize("{}"));
            Assert.AreEqual("type", ex.FieldName);
        }

        [Test]
        public void TestDeserializeNonObject()
        {
            Assert.Throws<JsonSerializationException>(() => Deserialize("[]"));
        }

        [Test]
        public void TestSerialize()
        {
            var mjson = new MjaiJson(JObject.FromObject(new { type = "dummy", actor = 1 }));
            Assert.AreEqual(@"{""type"":""dummy"",""actor"":1}", Serialize(mjson));
        }
    }
}
