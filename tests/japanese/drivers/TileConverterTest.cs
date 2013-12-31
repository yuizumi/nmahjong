using NUnit.Framework;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NMahjong.Japanese.Drivers
{
    [TestFixture]
    public class TileConverterTest : JsonSerializeTestBase
    {
        protected override IList<JsonConverter> GetConverters()
        {
            return List<JsonConverter>(TileConverter.Converter);
        }

        [Test]
        public void TestConvert()
        {
            Assert.AreEqual(@"""FE""", Serialize(FE));
        }
    }
}
