using NUnit.Framework;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NMahjong.Japanese.Drivers
{
    [TestFixture]
    public class RevealedMeldConverterTest : JsonSerializeTestBase
    {
        protected override IList<JsonConverter> GetConverters()
        {
            return List<JsonConverter>(
                TileConverter.Converter, AnnotatedTileConverter.Converter,
                RevealedMeldConverter.Converter);
        }

        [Test, TestCaseSource("TestConvertSource")]
        public void TestConvert(RevealedMeld value, string expected)
        {
            Assert.AreEqual(expected, Serialize(value));
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestConvertSource = {
            Data(OpenChow.Create(List(T5r, T6p), T7p, Player1),
                 @"{""Type"":""OpenChow"",""Feeder"":{""Id"":1},""AnnotatedTiles"":[" +
                 @"{""BaseTile"":""T5"",""Annotations"":1}," +
                 @"{""BaseTile"":""T6"",""Annotations"":0}," +
                 @"{""BaseTile"":""T7"",""Annotations"":4}]}"),
            Data(OpenPung.Create(List(T5r, T5p), T5p, Player1),
                 @"{""Type"":""OpenPung"",""Feeder"":{""Id"":1},""AnnotatedTiles"":[" +
                 @"{""BaseTile"":""T5"",""Annotations"":1}," +
                 @"{""BaseTile"":""T5"",""Annotations"":0}," +
                 @"{""BaseTile"":""T5"",""Annotations"":4}]}"),
            Data(OpenKong.Create(List(T5r, T5p, T5p), T5p, Player1),
                 @"{""Type"":""OpenKong"",""Feeder"":{""Id"":1},""AnnotatedTiles"":[" +
                 @"{""BaseTile"":""T5"",""Annotations"":1}," +
                 @"{""BaseTile"":""T5"",""Annotations"":0}," +
                 @"{""BaseTile"":""T5"",""Annotations"":0}," +
                 @"{""BaseTile"":""T5"",""Annotations"":4}]}"),
            Data(ConcealedKong.Create(List(T5r, T5p, T5p, T5p)),
                 @"{""Type"":""ConcealedKong"",""AnnotatedTiles"":[" +
                 @"{""BaseTile"":""T5"",""Annotations"":1}," +
                 @"{""BaseTile"":""T5"",""Annotations"":0}," +
                 @"{""BaseTile"":""T5"",""Annotations"":0}," +
                 @"{""BaseTile"":""T5"",""Annotations"":0}]}"),
            Data(ExtendedKong.Create(OpenPung.Create(List(T5r, T5p), T5p, Player1), T5p),
                 @"{""Type"":""ExtendedKong"",""Feeder"":{""Id"":1},""AnnotatedTiles"":[" +
                 @"{""BaseTile"":""T5"",""Annotations"":1}," +
                 @"{""BaseTile"":""T5"",""Annotations"":0}," +
                 @"{""BaseTile"":""T5"",""Annotations"":4}," +
                 @"{""BaseTile"":""T5"",""Annotations"":8}]}"),
        };
        #pragma warning restore 414
    }
}
