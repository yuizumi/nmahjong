using NUnit.Framework;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NMahjong.Japanese.Drivers
{
    [TestFixture]
    public class AnnotatedTileConverterTest : JsonSerializeTestBase
    {
        protected override IList<JsonConverter> GetConverters()
        {
            return List<JsonConverter>(
                TileConverter.Converter, AnnotatedTileConverter.Converter);
        }

        [Test, TestCaseSource("TestConvertSource")]
        public void TestConvert(AnnotatedTile value, string expected)
        {
            Assert.AreEqual(expected, Serialize(value));
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestConvertSource = {
            Data(FEp, @"{""BaseTile"":""FE"",""Annotations"":0}"),
            Data(Drawn(T5r), @"{""BaseTile"":""T5"",""Annotations"":3}"),
        };
        #pragma warning restore 414
    }
}
