using NUnit.Framework;
using System;
using System.Linq;
using NMahjong.Japanese;
using NMahjong.Testing;

namespace NMahjong.Drivers.Mjai
{
    [TestFixture]
    public class MjaiMessageBuilderTest : TestHelperWithRevealedMelds
    {
        MjaiMessageBuilder mBuilder;

        [SetUp]
        public void Setup()
        {
            mBuilder = new MjaiMessageBuilder(1);
        }

        [Test]
        public void TestNull()
        {
            const string expected = @"{""type"":""none""}";
            Assert.AreEqual(expected, mBuilder.Build(null));
        }

        [Test]
        public void TestNone()
        {
            var mjaiAction = new MjaiAction(Actions.None(), Player3, S6p);
            const string expected = @"{""type"":""none""}";
            Assert.AreEqual(expected, mBuilder.Build(mjaiAction));
        }

        [Test]
        public void TestDiscardNonDrawn()
        {
            var mjaiAction = new MjaiAction(Actions.Discard(S7p), Player0, W3p);
            const string expected = @"{""type"":""dahai"",""actor"":1,""pai"":""7s""," +
                @"""tsumogiri"":false}";
            Assert.AreEqual(expected, mBuilder.Build(mjaiAction));
        }

        [Test]
        public void TestDiscardDrawn()
        {
            var mjaiAction = new MjaiAction(Actions.Discard(Drawn(S7p)), Player0, W3p);
            const string expected = @"{""type"":""dahai"",""actor"":1,""pai"":""7s""," +
                @"""tsumogiri"":true}";
            Assert.AreEqual(expected, mBuilder.Build(mjaiAction));
        }

        [Test]
        public void TestRiichi()
        {
            var mjaiAction = new MjaiAction(Actions.Riichi(), Player0, W3p);
            const string expected = @"{""type"":""reach"",""actor"":1}";
            Assert.AreEqual(expected, mBuilder.Build(mjaiAction));
        }

        [Test]
        public void TestMahjongSelfDraw()
        {
            var mjaiAction = new MjaiAction(Actions.Mahjong(), Player0, W2p);
            const string expected = @"{""type"":""hora"",""actor"":1,""target"":1,""pai"":""2m""}";
            Assert.AreEqual(expected, mBuilder.Build(mjaiAction));
        }

        [Test]
        public void TestMahjongDiscard()
        {
            var mjaiAction = new MjaiAction(Actions.Mahjong(), Player3, S7p);
            const string expected = @"{""type"":""hora"",""actor"":1,""target"":0,""pai"":""7s""}";
            Assert.AreEqual(expected, mBuilder.Build(mjaiAction));
        }

        [Test]
        public void TestAbortiveDraw()
        {
            var mjaiAction = new MjaiAction(Actions.AbortiveDraw(), Player0, W3p);
            const string expected = @"{""type"":""ryukyoku"",""actor"":1," +
                @"""reason"":""kyushukyuhai""}";
            Assert.AreEqual(expected, mBuilder.Build(mjaiAction));
        }

        [Test]
        public void TestPung()
        {
            var action = Actions.Pung(OPung(S5p, S5p, S5r, Player1));
            var mjaiAction = new MjaiAction(action, Player1, S5r);
            const string expected = @"{""type"":""pon"",""actor"":1,""target"":2,""pai"":""5sr""," +
                @"""consumed"":[""5s"",""5s""]}";
            Assert.AreEqual(expected, mBuilder.Build(mjaiAction));
        }

        [Test]
        public void TestChow()
        {
            var action = Actions.Chow(OChow(T5p, T6p, T4p, Player3));
            var mjaiAction = new MjaiAction(action, Player3, T4p);
            const string expected = @"{""type"":""chi"",""actor"":1,""target"":0,""pai"":""4p""," +
                @"""consumed"":[""5p"",""6p""]}";
            Assert.AreEqual(expected, mBuilder.Build(mjaiAction));
        }


        [Test]
        public void TestOpenKong()
        {
            var action = Actions.OpenKong(OKong(W5p, W5p, W5r, W5p, Player2));
            var mjaiAction = new MjaiAction(action, Player2, W5p);
            const string expected = @"{""type"":""daiminkan"",""actor"":1,""target"":3," +
                @"""pai"":""5m"",""consumed"":[""5m"",""5m"",""5mr""]}";
            Assert.AreEqual(expected, mBuilder.Build(mjaiAction));
        }

        [Test]
        public void TestConcealedKong()
        {
            var action = Actions.ConcealedKong(CKong(FNp, FNp, FNp, FNp));
            var mjaiAction = new MjaiAction(action, Player0, JFp);
            const string expected = @"{""type"":""ankan"",""actor"":1," +
                @"""consumed"":[""N"",""N"",""N"",""N""]}";
            Assert.AreEqual(expected, mBuilder.Build(mjaiAction));
        }

        [Test]
        public void TestExtendedKong()
        {
            var action = Actions.ExtendedKong(EKong(W6p, W6p, W6p, Player2, W6p));
            var mjaiAction = new MjaiAction(action, Player0, JFp);
            const string expected = @"{""type"":""kakan"",""actor"":1,""pai"":""6m""," +
                @"""consumed"":[""6m"",""6m"",""6m""]}";
            Assert.AreEqual(expected, mBuilder.Build(mjaiAction));
        }
    }
}
