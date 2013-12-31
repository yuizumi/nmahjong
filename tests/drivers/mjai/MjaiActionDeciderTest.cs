using NUnit.Framework;
using NSubstitute;
using NMahjong.Base;
using NMahjong.Japanese;
using NMahjong.Japanese.Engine;
using NMahjong.Testing;

namespace NMahjong.Drivers.Mjai
{
    [TestFixture]
    public class MjaiActionDeciderTest : TestHelperWithRevealedMelds
    {
        private static readonly IPlayerAction DummyAction =
            Substitute.For<IPlayerAction>();

        private MjaiActionDecider mDecider;
        private EventSender mEventSender;
        private Intelligence mIntelligence;

        [SetUp]
        public void Setup()
        {
            mEventSender = new EventSender();
            IntelligenceArgs args = new IntelligenceArgs(Substitute.For<IGameState>(),
                                                         Substitute.For<IRuleAdvisor>());
            mIntelligence = Substitute.For<Intelligence>(args);
            mDecider = new MjaiActionDecider(mIntelligence);
            mDecider.RegisterHandlers(mEventSender);
            mIntelligence.OnTurn().Returns(DummyAction);
            mIntelligence.OnRiichi().Returns(DummyAction);
            mIntelligence.OnDiscard(Player0, null).ReturnsForAnyArgs(DummyAction);
            mIntelligence.OnKong(Player0, null).ReturnsForAnyArgs(DummyAction);
        }

        [Test]
        public void TestOnTileDiscardedSelf()
        {
            mEventSender.OnTileDiscarded(new TileDiscardedEventArgs(Player0, Drawn(T5r)));
            mIntelligence.DidNotReceiveWithAnyArgs().OnDiscard(Player0, null);
        }

        [Test]
        public void TestOnTileDiscardedElse()
        {
            mEventSender.OnTileDiscarded(new TileDiscardedEventArgs(Player3, Drawn(T5r)));
            mIntelligence.Received(1).OnDiscard(Player3, Drawn(T5r));
            MjaiAction mjaiAction = mDecider.GetNextAction();
            Assert.AreEqual(Player3, mjaiAction.Target);
            Assert.AreEqual(T5r, mjaiAction.PlayedTile);
            Assert.AreEqual(DummyAction, mjaiAction.Action);
        }

        [Test]
        public void TestOnTileDrawnSelf()
        {
            mEventSender.OnTileDrawn(new TileDrawnEventArgs(Player0, T5r));
            mIntelligence.Received(1).OnTurn();
            MjaiAction mjaiAction = mDecider.GetNextAction();
            Assert.AreEqual(Player0, mjaiAction.Target);
            Assert.AreEqual(T5r, mjaiAction.PlayedTile);
            Assert.AreEqual(DummyAction, mjaiAction.Action);
        }

        [Test]
        public void TestOnTileDrawnElse()
        {
            mEventSender.OnTileDrawn(new TileDrawnEventArgs(Player3, T5r));
            mIntelligence.DidNotReceive().OnTurn();
        }

        [Test]
        public void TestOnRiichiSelf()
        {
            mEventSender.OnRiichiDeclared(new RiichiEventArgs(Player0));
            mIntelligence.Received(1).OnRiichi();
            MjaiAction mjaiAction = mDecider.GetNextAction();
            Assert.AreEqual(Player0, mjaiAction.Target);
            Assert.AreEqual(null, mjaiAction.PlayedTile);
            Assert.AreEqual(DummyAction, mjaiAction.Action);
        }

        [Test]
        public void TestOnRiichiElse()
        {
            mEventSender.OnRiichiDeclared(new RiichiEventArgs(Player3));
            mIntelligence.DidNotReceive().OnRiichi();
        }

        [Test]
        public void TestOnOpenPungSelf()
        {
            OpenPung pung = OPung(FEp, FEp, FEp, Player2);
            mEventSender.OnMeldRevealed(new MeldRevealedEventArgs(Player0, pung));
            mIntelligence.Received(1).OnTurn();
            MjaiAction mjaiAction = mDecider.GetNextAction();
            Assert.AreEqual(Player0, mjaiAction.Target);
            Assert.AreEqual(null, mjaiAction.PlayedTile);
            Assert.AreEqual(DummyAction, mjaiAction.Action);
        }

        [Test]
        public void TestOnOpenPungElse()
        {
            OpenPung pung = OPung(FEp, FEp, FEp, Player2);
            mEventSender.OnMeldRevealed(new MeldRevealedEventArgs(Player3, pung));
            mIntelligence.DidNotReceive().OnTurn();
        }

        [Test]
        public void TestOnOpenChowSelf()
        {
            OpenChow chow = OChow(T6p, T7p, T5r, Player2);
            mEventSender.OnMeldRevealed(new MeldRevealedEventArgs(Player0, chow));
            mIntelligence.Received(1).OnTurn();
            MjaiAction mjaiAction = mDecider.GetNextAction();
            Assert.AreEqual(Player0, mjaiAction.Target);
            Assert.AreEqual(null, mjaiAction.PlayedTile);
            Assert.AreEqual(DummyAction, mjaiAction.Action);
        }

        [Test]
        public void TestOnOpenChowElse()
        {
            OpenChow chow = OChow(T6p, T7p, T5r, Player2);
            mEventSender.OnMeldRevealed(new MeldRevealedEventArgs(Player3, chow));
            mIntelligence.DidNotReceive().OnTurn();
        }

        [Test]
        public void TestOnOpenKongSelf()
        {
            OpenKong kong = OKong(FEp, FEp, FEp, FEp, Player2);
            mEventSender.OnMeldRevealed(new MeldRevealedEventArgs(Player0, kong));
            mIntelligence.DidNotReceive().OnTurn();
            mIntelligence.DidNotReceiveWithAnyArgs().OnKong(Player0, null);
        }

        [Test]
        public void TestOnOpenKongElse()
        {
            OpenKong kong = OKong(FEp, FEp, FEp, FEp, Player2);
            mEventSender.OnMeldRevealed(new MeldRevealedEventArgs(Player3, kong));
            mIntelligence.DidNotReceive().OnTurn();
            mIntelligence.DidNotReceiveWithAnyArgs().OnKong(Player0, null);
        }

        [Test]
        public void TestOnConcealedKongSelf()
        {
            ConcealedKong kong = CKong(FEp, FEp, FEp, FEp);
            mEventSender.OnMeldRevealed(new MeldRevealedEventArgs(Player0, kong));
            mIntelligence.DidNotReceive().OnTurn();
            mIntelligence.DidNotReceiveWithAnyArgs().OnKong(Player0, null);
        }

        [Test]
        public void TestOnConcealedKongElse()
        {
            // TODO(yuizumi): Allow robbing of a concealed kong for Thirteen Orphans
            // once it is implemented in the mjai server.
            ConcealedKong kong = CKong(FEp, FEp, FEp, FEp);
            mEventSender.OnMeldRevealed(new MeldRevealedEventArgs(Player3, kong));
            mIntelligence.DidNotReceive().OnTurn();
            mIntelligence.DidNotReceiveWithAnyArgs().OnKong(Player0, null);
        }

        [Test]
        public void TestOnExtendedKongSelf()
        {
            mEventSender.OnMeldExtended(new MeldExtendedEventArgs(Player0, FEp));
            mIntelligence.DidNotReceive().OnTurn();
            mIntelligence.DidNotReceiveWithAnyArgs().OnKong(Player0, null);
        }

        [Test]
        public void TestOnExtendedKongElse()
        {
            mEventSender.OnMeldExtended(new MeldExtendedEventArgs(Player3, FEp));
            mIntelligence.DidNotReceive().OnTurn();
            mIntelligence.Received(1).OnKong(Player3, FEp);
            MjaiAction mjaiAction = mDecider.GetNextAction();
            Assert.AreEqual(Player3, mjaiAction.Target);
            Assert.AreEqual(FEp, mjaiAction.PlayedTile);
            Assert.AreEqual(DummyAction, mjaiAction.Action);
        }

        [Test, TestCaseSource("TestCreateActionValidSource")]
        public void TestCreateActionValid(IPlayerAction action, PlayerId player,
                                          CanonicalTile playedTile)
        {
            Assert.DoesNotThrow(
                () => MjaiActionDecider.CreateAction(action, player, playedTile));
        }

        [Test, TestCaseSource("TestCreateActionInvalidSource")]
        public void TestCreateActionInvalid(IPlayerAction action, PlayerId player,
                                            CanonicalTile playedTile)
        {
            Assert.Throws<InvalidActionException>(
                () => MjaiActionDecider.CreateAction(action, player, playedTile));
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestCreateActionValidSource = {
            Data(Actions.Pung(OPung(FEp, FEp, FEp, Player2)), Player2, FEp),
            Data(Actions.Chow(OChow(T6p, T7p, T5r, Player2)), Player2, T5r),
            Data(Actions.OpenKong(OKong(FEp, FEp, FEp, FEp, Player2)), Player2, FEp),
            Data(Actions.ExtendedKong(EKong(FEp, FEp, FEp, Player2, FEp)), Player0, JFp),
            Data(Actions.ConcealedKong(CKong(FEp, FEp, FEp, FEp)), Player0, FEp),
        };

        private static readonly TestCaseData[]
        TestCreateActionInvalidSource = {
            Data(Actions.Pung(OPung(FEp, FEp, FEp, Player2)), Player1, FEp),
            Data(Actions.Pung(OPung(FEp, FEp, FEp, Player2)), Player2, JFp),
            Data(Actions.Chow(OChow(T6p, T7p, T5r, Player2)), Player1, T5r),
            Data(Actions.Chow(OChow(T6p, T7p, T5r, Player2)), Player2, JFp),
            Data(Actions.OpenKong(OKong(FEp, FEp, FEp, FEp, Player2)), Player1, FEp),
            Data(Actions.OpenKong(OKong(FEp, FEp, FEp, FEp, Player2)), Player2, JFp),
            Data(null, Player0, FEp),
        };
        #pragma warning restore 414
    }
}
