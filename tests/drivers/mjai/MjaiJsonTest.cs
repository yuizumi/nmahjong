using NUnit.Framework;
using System;
using System.Collections.Generic;
using NMahjong.Aux;
using NMahjong.Base;
using NMahjong.Japanese;
using NMahjong.Testing;

namespace NMahjong.Drivers.Mjai
{
    [TestFixture]
    public class MjaiJsonTest : TestHelperWithAnnotatedTiles
    {
        [Test, TestCaseSource("TestParseInvalidSource")]
        public void TestParseInvalid(string message, Type exception)
        {
            Assert.Throws(exception, () => MjaiJson.Parse(message));
        }

        #pragma warning disable 414
        private static readonly
        TestCaseData[] TestParseInvalidSource = {
            // Invalid JSON objects.
            Data(@"[""type"":""none""]", typeof(MjaiMalformedJsonException)),
            Data(@"""none""", typeof(MjaiMalformedJsonException)),
            // Missing "type".
            Data(@"{}", typeof(MjaiMissingFieldException)),
            // Invalid "type".
            Data(@"{""type"":[""none""]}", typeof(MjaiInvalidFieldException)),
        };
        #pragma warning restore 414

        [Test]
        public void TestParseErrorResponse()
        {
            var ex = Assert.Throws<MjaiErrorResponseException>(
                () => MjaiJson.Parse(@"{""type"":""error"",""message"":""I hate you.""}"));
            Assert.AreEqual("I hate you.", ex.ErrorMessage);
        }

        [Test]
        public void TestSerialize()
        {
            Assert.AreEqual(@"{""type"":""none""}", MjaiJson.Serialize(new { type = "none" }));
        }

        [Test]
        public void TestSerializeTile()
        {
            Assert.AreEqual(@"{""type"":""dummy"",""pai"":""5pr""}",
                            MjaiJson.Serialize(new { type = "dummy", pai = T5r }));
        }

        [Test]
        public void TestType()
        {
            MjaiJson json = MjaiJson.Parse(@"{""type"":""none""}");
            Assert.AreEqual("none", json.Type);
        }

        [Test]
        public void TestHas()
        {
            MjaiJson json = MjaiJson.Parse(@"{""type"":""dummy"",""actor"":1}");
            Assert.IsTrue(json.Has("actor"));
            Assert.IsFalse(json.Has("target"));
        }

        [Test]
        public void TestGet()
        {
            MjaiJson json = MjaiJson.Parse(@"{""type"":""dummy"",""actor"":1}");
            Assert.AreEqual(1, json.Get<Int32>("actor"));
        }

        [Test]
        public void TestGetArray()
        {
            MjaiJson json = MjaiJson.Parse(@"{""type"":""dummy"",""deltas"":[0,-1000,0,1000]}");
            Assert.AreEqual(new [] { 0, -1000, 0, 1000 }, json.Get<List<Int32>>("deltas"));
        }

        [Test]
        public void TestGetMissingField()
        {
            MjaiJson json = MjaiJson.Parse(@"{""type"":""dummy"",""actor"":1}");
            var ex = Assert.Throws<MjaiMissingFieldException>(
                () => json.Get<Int32>("target"));
            Assert.AreEqual(json, ex.MjaiJson);
            Assert.AreEqual("target", ex.FieldName);
        }

        [Test]
        public void TestGetTypeMismatch()
        {
            MjaiJson json = MjaiJson.Parse(@"{""type"":""dummy"",""actor"":1}");
            var ex = Assert.Throws<MjaiInvalidFieldException>(
                () => json.Get<DateTime>("actor"));
            Assert.AreEqual(json, ex.MjaiJson);
            Assert.AreEqual("actor", ex.FieldName);
        }

        [Test]
        public void TestGetTile()
        {
            MjaiJson json = MjaiJson.Parse(@"{""type"":""dummy"",""pai"":""5pr""}");
            Assert.AreEqual(T5r, json.Get<CanonicalTile>("pai"));
        }

        [Test]
        public void TestGetTileInvalid()
        {
            MjaiJson json = MjaiJson.Parse(@"{""type"":""dummy"",""pai"":""BAD""}");
            var ex = Assert.Throws<MjaiInvalidFieldException>(
                () => json.Get<CanonicalTile>("pai"));
            Assert.AreEqual(json, ex.MjaiJson);
            Assert.AreEqual("pai", ex.FieldName);
        }

        [Test]
        public void TestGetWind()
        {
            MjaiJson json = MjaiJson.Parse(@"{""type"":""dummy"",""bakaze"":""E""}");
            Assert.AreEqual(Wind.East, json.Get<Wind>("bakaze"));
        }

        [Test]
        public void TestGetWindInvalid()
        {
            MjaiJson json = MjaiJson.Parse(@"{""type"":""dummy"",""bakaze"":""P""}");
            var ex = Assert.Throws<MjaiInvalidFieldException>(
                () => json.Get<Wind>("bakaze"));
            Assert.AreEqual(json, ex.MjaiJson);
            Assert.AreEqual("bakaze", ex.FieldName);
        }

        [Test]
        public void TestGetDrawHand()
        {
            MjaiJson json = MjaiJson.Parse(@"{""type"":""dummy"",""reason"":""fanpai""}");
            Assert.AreEqual(DrawHand.Exhaustive, json.Get<DrawHand>("reason"));
        }

        [Test]
        public void TestGetDrawHandInvalid()
        {
            MjaiJson json = MjaiJson.Parse(@"{""type"":""dummy"",""reason"":""chombo""}");
            var ex = Assert.Throws<MjaiInvalidFieldException>(
                () => json.Get<DrawHand>("reason"));
            Assert.AreEqual(json, ex.MjaiJson);
            Assert.AreEqual("reason", ex.FieldName);
        }
    }
}