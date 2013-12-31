using NUnit.Framework;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using NMahjong.Base;
using NMahjong.Testing;

namespace NMahjong.Japanese.Engine
{
    [TestFixture]
    public class GameStateImplTest : TestHelperWithRevealedMelds
    {
        private GameStateImpl mGameState;
        private EventSender mSender;
        private Quad<IPlayerHandInternal> mPlayerHands;
        private WallImpl mWallImpl;

        [SetUp]
        public void Setup()
        {
            mGameState = new GameStateImpl(Quad.Of("Alice", "Brian", "Carol", "David"),
                                           Quad.Of(25000, 25000, 25000, 25000));
            mSender = new EventSender();
            mGameState.RegisterHandlers(mSender);
        }

        private void SetupHand(HandState handState)
        {
            mPlayerHands = Quad.Of(Substitute.For<IPlayerHandInternal>(),
                                   Substitute.For<IPlayerHandInternal>(),
                                   Substitute.For<IPlayerHandInternal>(),
                                   Substitute.For<IPlayerHandInternal>());
            mWallImpl = Substitute.For<WallImpl>();
            mSender.OnHandStarting(new HandStartingEventArgs(mWallImpl, Player0, Wind.East, 1));
            mGameState.SetHandStateForTest(handState);
            for (int i = 0; i < 4; i++) mGameState.PlayerImpls[i].SetTiles(mPlayerHands[i]);
        }

        [Test]
        public void TestCtor()
        {
            Assert.AreEqual(HandState.NotStarted, mGameState.HandState);
            Assert.AreEqual("Alice", mGameState.Players[0].Name);
            Assert.AreEqual(25000, mGameState.Players[0].Score);
            Assert.AreEqual("Brian", mGameState.Players[1].Name);
            Assert.AreEqual(25000, mGameState.Players[1].Score);
            Assert.AreEqual("Carol", mGameState.Players[2].Name);
            Assert.AreEqual(25000, mGameState.Players[2].Score);
            Assert.AreEqual("David", mGameState.Players[3].Name);
            Assert.AreEqual(25000, mGameState.Players[3].Score);
        }

        [Test]
        public void TestOnHandStarting()
        {
            WallImpl dummyWall = Substitute.For<WallImpl>();
            mSender.OnHandStarting(new HandStartingEventArgs(
                dummyWall, Player3, Wind.South, 2));
            Assert.AreEqual(HandState.Starting, mGameState.HandState);
            Assert.AreEqual(Player3, mGameState.Turn);
            Assert.AreEqual(dummyWall, mGameState.Wall);
            Assert.AreEqual(Player3, mGameState.Dealer);
            Assert.AreEqual(Wind.South, mGameState.PrevailingWind);
            Assert.AreEqual(2, mGameState.HandNumber);
            Assert.AreEqual(Wind.South, mGameState.Players[0].SeatWind);
            Assert.AreEqual(Wind.West , mGameState.Players[1].SeatWind);
            Assert.AreEqual(Wind.North, mGameState.Players[2].SeatWind);
            Assert.AreEqual(Wind.East , mGameState.Players[3].SeatWind);
            Assert.AreEqual(Enumerable.Empty<Dora>(), mGameState.Dora);
        }

        [Test]
        public void TestOnHandStarted()
        {
            SetupHand(HandState.Starting);
            mSender.OnHandStarted(EventArgs.Empty);
            Assert.AreEqual(HandState.Playing, mGameState.HandState);
        }

        [Test]
        public void TestOnHandEnded()
        {
            SetupHand(HandState.Playing);
            mSender.OnHandEnded(EventArgs.Empty);
            Assert.AreEqual(HandState.Ended, mGameState.HandState);
        }

        [Test]
        public void TestOnDoraAdded()
        {
            SetupHand(HandState.Playing);
            Dora dora = Dora.Next(T5r);
            mSender.OnDoraAdded(new DoraAddedEventArgs(dora));
            mWallImpl.Received(1).OnDoraAdded(dora);
            Assert.AreEqual(new [] { dora }, mGameState.Dora);
        }

        [Test]
        public void TestOnMeldExtended()
        {
            SetupHand(HandState.Playing);
            // Setup.
            var pung1 = OPung(FEp, FEp, FEp, Player3);
            var pung2 = OPung(T5r, T5p, T5p, Player2);
            var chow  = OChow(W3p, W4p, W5p, Player0);
            mGameState.PlayerImpls[1].SetMeldsForTest(pung1, pung2, chow);
            // Test.
            mSender.OnMeldExtended(new MeldExtendedEventArgs(Player1, T5p));
            Assert.AreEqual(Player1, mGameState.Turn);
            mPlayerHands[1].Received(1).Exclude(Arg.Is<IEnumerable<CanonicalTile>>(
                seq => seq.SequenceEqual(new [] { T5p })));
            Assert.AreEqual(3, mGameState.Players[1].Melds.Count);
            MeldAssert.AreIdentical(pung1, mGameState.Players[1].Melds[0]);
            MeldAssert.AreIdentical(EKong(T5r, T5p, T5p, Player2, T5p),
                                    mGameState.Players[1].Melds[1]);
            MeldAssert.AreIdentical(chow , mGameState.Players[1].Melds[2]);
        }

        [Test]
        public void TestOnMeldRevealedConcealed()
        {
            SetupHand(HandState.Playing);
            // Setup.
            var pung = OPung(JFp, JFp, JFp, Player2);
            var kong = CKong(T5r, T5p, T5p, T5p);
            mGameState.PlayerImpls[1].SetMeldsForTest(pung);
            // Test.
            mSender.OnMeldRevealed(new MeldRevealedEventArgs(Player1, kong));
            Assert.AreEqual(Player1, mGameState.Turn);
            mPlayerHands[1].Received(1).Exclude(Arg.Is<IEnumerable<CanonicalTile>>(
                seq => seq.SequenceEqual(new [] { T5r, T5p, T5p, T5p })));
            Assert.AreEqual(2, mGameState.Players[1].Melds.Count);
            MeldAssert.AreIdentical(pung, mGameState.Players[1].Melds[0]);
            MeldAssert.AreIdentical(kong, mGameState.Players[1].Melds[1]);
        }

        [Test]
        public void TestOnMeldRevealedOpen()
        {
            SetupHand(HandState.Playing);
            // Setup.
            var pung = OPung(JFp, JFp, JFp, Player2);
            var chow = OChow(T5r, T7p, T6p, Player0);
            mGameState.PlayerImpls[1].SetMeldsForTest(pung);
            mGameState.PlayerImpls[0].SetDiscardsForTest(FEp, T6p);
            // Test.
            mSender.OnMeldRevealed(new MeldRevealedEventArgs(Player1, chow));
            Assert.AreEqual(Player1, mGameState.Turn);
            mPlayerHands[1].Received(1).Exclude(Arg.Is<IEnumerable<CanonicalTile>>(
                seq => seq.SequenceEqual(new [] { T5r, T7p })));
            Assert.AreEqual(2, mGameState.Players[1].Melds.Count);
            MeldAssert.AreIdentical(pung, mGameState.Players[1].Melds[0]);
            MeldAssert.AreIdentical(chow, mGameState.Players[1].Melds[1]);
            Assert.AreEqual(new [] { FEp, Claimed(T6p) }, mGameState.Players[0].Discards);
        }

        [Test]
        public void TestOnPlayerHandUpdated()
        {
            SetupHand(HandState.Starting);
            var tiles = PlayerHand.Hidden(13);
            mSender.OnPlayerHandUpdated(new PlayerHandUpdatedEventArgs(Player1, tiles));
            Assert.AreEqual(Player0, mGameState.Turn);  // Turn does not change.
            Assert.AreEqual(tiles, mGameState.Players[1].Tiles);
        }

        [Test]
        public void TestOnScoreUpdated()
        {
            SetupHand(HandState.Starting);
            mSender.OnScoreUpdated(new ScoreUpdatedEventArgs(Player1, 13000, -12000));
            Assert.AreEqual(Player0, mGameState.Turn);  // Turn does not change.
            Assert.AreEqual(13000, mGameState.Players[1].Score);
        }

        [Test]
        public void TestOnSticksUpdated()
        {
            SetupHand(HandState.Starting);
            mSender.OnSticksUpdated(new SticksUpdatedEventArgs(2, 1));
            Assert.AreEqual(2, mGameState.Counters);
            Assert.AreEqual(1, mGameState.RiichiSticks);
        }

        [Test]
        public void TestOnTileDiscarded()
        {
            SetupHand(HandState.Playing);
            mGameState.PlayerImpls[1].SetDiscardsForTest(Claimed(JFp));
            mSender.OnTileDiscarded(new TileDiscardedEventArgs(Player1, Drawn(T5r)));
            Assert.AreEqual(Player1, mGameState.Turn);
            mPlayerHands[1].Received(1).Discard(Drawn(T5r));
            Assert.AreEqual(new [] { Claimed(JFp), Drawn(T5r) }, mGameState.Players[1].Discards);
        }

        [Test]
        public void TestOnTileDrawnShowed()
        {
            SetupHand(HandState.Playing);
            mSender.OnTileDrawn(new TileDrawnEventArgs(Player1, T5r));
            Assert.AreEqual(Player1, mGameState.Turn);
            mWallImpl.Received(1).OnTileDrawn(T5r);
            mPlayerHands[1].Received(1).Draw(T5r);
        }

        [Test]
        public void TestOnTileDrawnHidden()
        {
            SetupHand(HandState.Playing);
            mSender.OnTileDrawn(new TileDrawnEventArgs(Player1, null));
            Assert.AreEqual(Player1, mGameState.Turn);
            mWallImpl.Received(1).OnTileDrawn(null);
            mPlayerHands[1].Received(1).Draw(null);
        }
    }
}
