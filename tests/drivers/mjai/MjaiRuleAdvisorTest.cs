using NUnit.Framework;
using NSubstitute;
using NMahjong.Aux;
using NMahjong.Base;
using NMahjong.Japanese;
using NMahjong.Japanese.Engine;
using NMahjong.Testing;

namespace NMahjong.Drivers.Mjai
{
    [TestFixture]
    public class MjaiRuleAdvisorTest : TestHelperWithRevealedMelds
    {
        private IGameState mGameState;
        private IPlayerState mPlayerState;
        private MjaiRuleAdvisor mRuleAdvisor;

        [SetUp]
        public void Setup()
        {
            mGameState = Substitute.For<IGameState>();
            mPlayerState = Substitute.For<IPlayerState>();
            mGameState.Players.Returns(Quad.Of(mPlayerState, null, null, null));
            mRuleAdvisor = new MjaiRuleAdvisor(mGameState, 1);
        }

        private void SetPlayerTiles(params AnnotatedTile[] tiles)
        {
            mPlayerState.Tiles.Returns(new MockPlayerHand(tiles));
        }

        private void SetPlayerMelds(params RevealedMeld[] melds)
        {
            mPlayerState.Melds.Returns(melds.AsReadOnlyView());
        }

        private void SetPlayerDiscards(params AnnotatedTile[] discards)
        {
            mPlayerState.Discards.Returns(discards.AsReadOnlyView());
        }

        private void SetMessage(string message)
        {
            mRuleAdvisor.SetMessage(MjaiJson.Parse(message));
        }

        private class MockPlayerHand : ReadOnlyListView<AnnotatedTile>, IPlayerHand
        {
            internal MockPlayerHand(AnnotatedTile[] tiles) : base(tiles)
            {
            }
        }


        //--------------------------------
        //  GetValidActions()

        [Test]
        public void TestGetValidActionsSelf()
        {
            mGameState.Turn.Returns(Player0);
            SetPlayerDiscards(FEp, JFp);
            SetPlayerTiles(T5p, T5p, T5r, T7p, Drawn(T7p));
            SetMessage(@"{""type"":""hoge""}");
            IPlayerAction[] expected = {
                Actions.Discard(T5p), Actions.Discard(T5r),
                Actions.Discard(T7p), Actions.Discard(Drawn(T7p)),
            };
            Assert.That(mRuleAdvisor.GetValidActions(),
                        Is.EquivalentTo(expected).Using(ActionIdentity.Comparer));
        }

        [Test]
        public void TestGetValidActionsElse()
        {
            mGameState.Turn.Returns(Player3);
            SetPlayerDiscards(FEp, JFp);
            SetPlayerTiles(T5p, T5p, T5r, T7p);
            SetMessage(@"{""type"":""hoge""}");
            IPlayerAction[] expected = {
                Actions.None()
            };
            Assert.That(mRuleAdvisor.GetValidActions(),
                        Is.EquivalentTo(expected).Using(ActionIdentity.Comparer));
        }

        [Test]
        public void TestGetValidActionsSelfPossibleActions()
        {
            mGameState.Turn.Returns(Player0);
            SetPlayerDiscards(FEp, JFp);
            SetPlayerTiles(T5p, T5p, T5r, T7p, Drawn(T7p));
            SetMessage(@"{
                ""type"":""dummy"",
                ""possible_actions"":[
                    {""type"":""hora"",""actor"":1,""target"":1,""pai"":""7p""}
                ]
            }");
            IPlayerAction[] expected = {
                Actions.Discard(T5p), Actions.Discard(T5r),
                Actions.Discard(T7p), Actions.Discard(Drawn(T7p)), Actions.Mahjong(),
            };
            Assert.That(mRuleAdvisor.GetValidActions(),
                        Is.EquivalentTo(expected).Using(ActionIdentity.Comparer));
        }

        [Test]
        public void TestGetValidActionsElsePossibleActions()
        {
            mGameState.Turn.Returns(Player3);
            SetPlayerDiscards(FEp, JFp);
            SetPlayerTiles(T5p, T5p, T5r, T7p);
            SetMessage(@"{
                ""type"":""dummy"",
                ""possible_actions"":[
                    {""type"":""chi"",""actor"":1,""target"":0,""pai"":""6p"",
                     ""consumed"":[""5p"",""7p""]},
                    {""type"":""chi"",""actor"":1,""target"":0,""pai"":""6p"",
                     ""consumed"":[""5pr"",""7p""]},
                    {""type"":""hora"",""actor"":1,""target"":0,""pai"":""6p""}
                ]
            }");
            IPlayerAction[] expected = {
                Actions.Chow(OChow(T5p, T7p, T6p, Player3)),
                Actions.Chow(OChow(T5r, T7p, T6p, Player3)),
                Actions.Mahjong(), Actions.None(),
            };
            Assert.That(mRuleAdvisor.GetValidActions(),
                        Is.EquivalentTo(expected).Using(ActionIdentity.Comparer));
        }

        [Test]
        public void TestGetValidActionsCannotDahai()
        {
            mGameState.Turn.Returns(Player0);
            SetPlayerDiscards(FEp, JFp);
            SetPlayerTiles(T5p, T5p, T5r, T7p, T7p);
            SetMessage(@"{
                ""type"":""dummy"",""cannot_dahai"":[""5p"",""5pr""]
            }");
            IPlayerAction[] expected = {
                Actions.Discard(T7p)
            };
            Assert.That(mRuleAdvisor.GetValidActions(),
                        Is.EquivalentTo(expected).Using(ActionIdentity.Comparer));
        }

        [Test]
        public void TestGetValidActionsRiichi()
        {
            mGameState.Turn.Returns(Player0);
            SetPlayerDiscards(FEp, Riichi(JFp));
            SetPlayerTiles(T5p, T5p, T5r, T7p, Drawn(T7p));
            SetMessage(@"{
                ""type"":""dummy"",
                ""possible_actions"":[
                    {""type"":""hora"",""actor"":1,""target"":1,""pai"":""7p""}
                ]
            }");
            IPlayerAction[] expected = {
                Actions.Discard(Drawn(T7p)), Actions.Mahjong(),
            };
            Assert.That(mRuleAdvisor.GetValidActions(),
                        Is.EquivalentTo(expected).Using(ActionIdentity.Comparer));
        }


        //--------------------------------
        //  ParseToPlayerAction()

        private IPlayerAction ParseToPlayerAction(string message)
        {
            return mRuleAdvisor.ParseToPlayerAction(MjaiJson.Parse(message));
        }

        [Test]
        public void TestParseToPung()
        {
            IPlayerAction action = ParseToPlayerAction(@"{
                ""type"":""pon"",""actor"":1,
                ""target"":2,""pai"":""5sr"",""consumed"":[""5s"",""5s""]}");
            ActionAssert.AreIdentical(
                Actions.Pung(OPung(S5p, S5p, S5r, Player1)), action);
        }

        [Test]
        public void TestParseToChow()
        {
            IPlayerAction action = ParseToPlayerAction(@"{
                ""type"":""chi"",""actor"":1,
                ""target"":0,""pai"":""4p"",""consumed"":[""5p"",""6p""]}");
            ActionAssert.AreIdentical(
                Actions.Chow(OChow(T5p, T6p, T4p, Player3)), action);
        }

        [Test]
        public void TestParseToExtendedKong()
        {
            SetPlayerMelds(OPung(S5p, S5p, S5r, Player1),
                           OPung(W6p, W6p, W6p, Player2),
                           OChow(T5p, T6p, T4p, Player3));
            IPlayerAction action = ParseToPlayerAction(@"{
                ""type"":""kakan"",""actor"":1,
                ""pai"":""6m"",""consumed"":[""6m"",""6m"",""6m""]}");
            ActionAssert.AreIdentical(
                Actions.ExtendedKong(EKong(W6p, W6p, W6p, Player2, W6p)), action);
        }

        [Test]
        public void TestParseToOpenKong()
        {
            IPlayerAction action = ParseToPlayerAction(@"{
                ""type"":""daiminkan"",""actor"":1,
                ""target"":3,""pai"":""5m"",""consumed"":[""5m"",""5m"",""5mr""]}");
            ActionAssert.AreIdentical(
                Actions.OpenKong(OKong(W5p, W5p, W5r, W5p, Player2)), action);
        }

        [Test]
        public void TestParseToConcealedKong()
        {
            IPlayerAction action = ParseToPlayerAction(@"{
                ""type"":""ankan"",""actor"":1,
                ""consumed"":[""N"",""N"",""N"",""N""]}");
            ActionAssert.AreIdentical(
                Actions.ConcealedKong(CKong(FNp, FNp, FNp, FNp)), action);
        }

        [Test]
        public void TestParseToRiichi()
        {
            IPlayerAction action = ParseToPlayerAction(@"{
                ""type"":""reach"",""actor"":1}");
            ActionAssert.AreIdentical(Actions.Riichi(), action);
        }

        [Test]
        public void TestParseToMahjong()
        {
            IPlayerAction action = ParseToPlayerAction(@"{
                ""type"":""hora"",""actor"":1,""target"":0,""pai"":""7s""}");
            ActionAssert.AreIdentical(Actions.Mahjong(), action);
        }

        [Test]
        public void TestParseToAbortiveDraw()
        {
            IPlayerAction action = ParseToPlayerAction(@"{
                ""type"":""ryukyoku"",""actor"":1,""reason"":""kyushukyuhai""}");
            ActionAssert.AreIdentical(Actions.AbortiveDraw(), action);
        }
    }
}
