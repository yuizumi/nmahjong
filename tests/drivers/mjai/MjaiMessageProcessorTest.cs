using NUnit.Framework;
using NSubstitute;
using System.Linq;
using NMahjong.Base;
using NMahjong.Japanese;
using NMahjong.Japanese.Engine;
using NMahjong.Testing;

namespace NMahjong.Drivers.Mjai
{
    [TestFixture]
    public class MjaiMessageProcessorTest : TestHelperWithRevealedMelds
    {
        private MjaiMessageProcessor mProcessor;
        private IEventSender mEventSender;

        [SetUp]
        public void Setup()
        {
            mEventSender = Substitute.For<IEventSender>();
            mProcessor = new MjaiMessageProcessor(mEventSender, 1);
        }

        [Test]
        public void TestStartKyoku()
        {
            const string message = @"{
                ""type"":""start_kyoku"",""bakaze"":""S"",""kyoku"":1,""honba"":2,""kyotaku"":3,
                ""oya"":0,""dora_marker"":""7s"",
                ""tehais"":[
                    [""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?""],
                    [""3m"",""4m"",""3p"",""5pr"",""7p"",""9p"",""4s"",""4s"",""5sr"",""7s"",
                     ""7s"",""W"",""N""],
                    [""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?""],
                    [""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?""]
                ]
            }";

            HandStartingEventArgs handStarting = null;
            mEventSender.OnHandStarting(
                Arg.Do<HandStartingEventArgs>(e => { handStarting = e; }));
            SticksUpdatedEventArgs sticksUpdated = null;
            mEventSender.OnSticksUpdated(
                Arg.Do<SticksUpdatedEventArgs>(e => { sticksUpdated = e; }));
            var tiles = new IPlayerHand[4];
            mEventSender.OnPlayerHandUpdated(
                Arg.Do<PlayerHandUpdatedEventArgs>(e => { tiles[e.Player.Id] = e.Tiles; }));
            DoraAddedEventArgs doraAdded = null;
            mEventSender.OnDoraAdded(
                Arg.Do<DoraAddedEventArgs>(e => { doraAdded = e; }));

            mProcessor.Process(MjaiJson.Parse(message));

            mEventSender.ReceivedWithAnyArgs(1).OnHandStarting(null);
            Assert.AreEqual(Wind.South, handStarting.PrevailingWind);
            Assert.AreEqual(1, handStarting.HandNumber);
            Assert.AreEqual(Player3, handStarting.Dealer);
            Assert.AreEqual(136 - 13 * 4 - 14, handStarting.Wall.Count);

            mEventSender.ReceivedWithAnyArgs(1).OnSticksUpdated(null);
            Assert.AreEqual(2, sticksUpdated.Counters);
            Assert.AreEqual(3, sticksUpdated.RiichiSticks);
            Assert.AreEqual(2, mProcessor.Counters);
            Assert.AreEqual(3, mProcessor.RiichiSticks);

            mEventSender.ReceivedWithAnyArgs(4).OnPlayerHandUpdated(null);
            Assert.AreEqual(new [] { W3p, W4p, T3p, T5r, T7p, T9p, S4p, S4p, S5r, S7p,
                                     S7p, FWp, FNp }, tiles[0]);
            Assert.AreEqual(Enumerable.Repeat<CanonicalTile>(null, 13), tiles[1]);
            Assert.AreEqual(Enumerable.Repeat<CanonicalTile>(null, 13), tiles[2]);
            Assert.AreEqual(Enumerable.Repeat<CanonicalTile>(null, 13), tiles[3]);

            mEventSender.ReceivedWithAnyArgs(1).OnDoraAdded(null);
            Assert.AreEqual(S7p, doraAdded.Dora.Indicator);
            Assert.AreEqual(S8, doraAdded.Dora.Tile);

            mEventSender.ReceivedWithAnyArgs(1).OnHandStarted(null);
        }

        [Test]
        public void TestEndKyoku()
        {
            const string message = @"{""type"":""end_kyoku""}";
            mProcessor.Process(MjaiJson.Parse(message));
            mEventSender.ReceivedWithAnyArgs(1).OnHandEnded(null);
        }

        [Test]
        public void TestTsumoShowed()
        {
            const string message = @"{
                ""type"":""tsumo"",""actor"":1,""pai"":""3m"",
            }";

            TileDrawnEventArgs eventArgs = null;
            mEventSender.OnTileDrawn(
                Arg.Do<TileDrawnEventArgs>(e => { eventArgs = e; }));
            mProcessor.Process(MjaiJson.Parse(message));
            mEventSender.ReceivedWithAnyArgs(1).OnTileDrawn(null);
            Assert.AreEqual(Player0, eventArgs.Player);
            Assert.AreEqual(W3p, eventArgs.Tile);
        }

        [Test]
        public void TestTsumoHidden()
        {
            const string message = @"{
                ""type"":""tsumo"",""actor"":0,""pai"":""?"",
            }";

            TileDrawnEventArgs eventArgs = null;
            mEventSender.OnTileDrawn(
                Arg.Do<TileDrawnEventArgs>(e => { eventArgs = e; }));
            mProcessor.Process(MjaiJson.Parse(message));
            mEventSender.ReceivedWithAnyArgs(1).OnTileDrawn(null);
            Assert.AreEqual(Player3, eventArgs.Player);
            Assert.AreEqual(null, eventArgs.Tile);
        }

        [Test]
        public void TestDahaiTedashi()
        {
            const string message = @"{
                ""type"":""dahai"",""actor"":1,""pai"":""7s"",""tsumogiri"":false
            }";

            TileDiscardedEventArgs eventArgs = null;
            mEventSender.OnTileDiscarded(
                Arg.Do<TileDiscardedEventArgs>(e => { eventArgs = e; }));
            mProcessor.Process(MjaiJson.Parse(message));
            mEventSender.ReceivedWithAnyArgs(1).OnTileDiscarded(null);
            Assert.AreEqual(Player0, eventArgs.Player);
            Assert.AreEqual(S7p, eventArgs.Discard);
        }

        [Test]
        public void TestDahaiTsumogiri()
        {
            const string message = @"{
                ""type"":""dahai"",""actor"":1,""pai"":""7s"",""tsumogiri"":true
            }";

            TileDiscardedEventArgs eventArgs = null;
            mEventSender.OnTileDiscarded(
                Arg.Do<TileDiscardedEventArgs>(e => { eventArgs = e; }));
            mProcessor.Process(MjaiJson.Parse(message));
            mEventSender.ReceivedWithAnyArgs(1).OnTileDiscarded(null);
            Assert.AreEqual(Player0, eventArgs.Player);
            Assert.AreEqual(Drawn(S7p), eventArgs.Discard);
        }

        [Test]
        public void TestPon()
        {
            const string message = @"{
                ""type"":""pon"",""actor"":0,""target"":1,""pai"":""5sr"",
                ""consumed"":[""5s"",""5s""]
            }";

            MeldRevealedEventArgs eventArgs = null;
            mEventSender.OnMeldRevealed(
                Arg.Do<MeldRevealedEventArgs>(e => { eventArgs = e; }));
            mProcessor.Process(MjaiJson.Parse(message));
            mEventSender.ReceivedWithAnyArgs(1).OnMeldRevealed(null);
            Assert.AreEqual(Player3, eventArgs.Player);
            MeldAssert.AreIdentical(OPung(S5p, S5p, S5r, Player0), eventArgs.Meld);
        }

        [Test]
        public void TestChi()
        {
            const string message = @"{
                ""type"":""chi"",""actor"":0,""target"":3,""pai"":""4p"",
                ""consumed"":[""5p"",""6p""]
            }";

            MeldRevealedEventArgs eventArgs = null;
            mEventSender.OnMeldRevealed(
                Arg.Do<MeldRevealedEventArgs>(e => { eventArgs = e; }));
            mProcessor.Process(MjaiJson.Parse(message));
            mEventSender.ReceivedWithAnyArgs(1).OnMeldRevealed(null);
            Assert.AreEqual(Player3, eventArgs.Player);
            MeldAssert.AreIdentical(OChow(T5p, T6p, T4p, Player2), eventArgs.Meld);
        }

        [Test]
        public void TestDaiminkan()
        {
            const string message = @"{
                ""type"":""daiminkan"",""actor"":3,""target"":1,""pai"":""5m"",
                ""consumed"":[""5m"",""5m"",""5mr""]
            }";

            MeldRevealedEventArgs eventArgs = null;
            mEventSender.OnMeldRevealed(
                Arg.Do<MeldRevealedEventArgs>(e => { eventArgs = e; }));
            mProcessor.Process(MjaiJson.Parse(message));
            mEventSender.ReceivedWithAnyArgs(1).OnMeldRevealed(null);
            Assert.AreEqual(Player2, eventArgs.Player);
            MeldAssert.AreIdentical(OKong(W5p, W5p, W5r, W5p, Player0), eventArgs.Meld);
        }

        [Test]
        public void TestAnkan()
        {
            const string message = @"{
                ""type"":""ankan"",""actor"":1,""consumed"":[""N"",""N"",""N"",""N""]
            }";

            MeldRevealedEventArgs eventArgs = null;
            mEventSender.OnMeldRevealed(
                Arg.Do<MeldRevealedEventArgs>(e => { eventArgs = e; }));
            mProcessor.Process(MjaiJson.Parse(message));
            mEventSender.ReceivedWithAnyArgs(1).OnMeldRevealed(null);
            Assert.AreEqual(Player0, eventArgs.Player);
            MeldAssert.AreIdentical(CKong(FNp, FNp, FNp, FNp), eventArgs.Meld);
        }

        [Test]
        public void TestKakan()
        {
            const string message = @"{
                ""type"":""kakan"",""actor"":0,""pai"":""6m"",
                ""consumed"":[""6m"",""6m"",""6m""]
            }";

            MeldExtendedEventArgs eventArgs = null;
            mEventSender.OnMeldExtended(
                Arg.Do<MeldExtendedEventArgs>(e => { eventArgs = e; }));
            mProcessor.Process(MjaiJson.Parse(message));
            mEventSender.ReceivedWithAnyArgs(1).OnMeldExtended(null);
            Assert.AreEqual(Player3, eventArgs.Player);
            Assert.AreEqual(W6p, eventArgs.Extender);
        }

        [Test]
        public void TestDora()
        {
            const string message = @"{""type"":""dora"",""dora_marker"":""8p""}";

            DoraAddedEventArgs eventArgs = null;
            mEventSender.OnDoraAdded(
                Arg.Do<DoraAddedEventArgs>(e => { eventArgs = e; }));
            mProcessor.Process(MjaiJson.Parse(message));
            mEventSender.ReceivedWithAnyArgs(1).OnDoraAdded(null);
            Assert.AreEqual(T8p, eventArgs.Dora.Indicator);
            Assert.AreEqual(T9, eventArgs.Dora.Tile);
        }

        [Test]
        public void TestReach()
        {
            const string reach = @"{
                ""type"":""reach"",""actor"":1
            }";
            const string dahai = @"{
                ""type"":""dahai"",""actor"":1,""pai"":""7s"",""tsumogiri"":false
            }";

            RiichiEventArgs riichiDeclared = null;
            mEventSender.OnRiichiDeclared(
                Arg.Do<RiichiEventArgs>(e => { riichiDeclared = e; }));
            mProcessor.Process(MjaiJson.Parse(reach));
            mEventSender.ReceivedWithAnyArgs(1).OnRiichiDeclared(null);
            Assert.IsTrue(mProcessor.IsRiichi);
            Assert.AreEqual(Player0, riichiDeclared.Player);

            TileDiscardedEventArgs tileDiscarded = null;
            mEventSender.OnTileDiscarded(
                Arg.Do<TileDiscardedEventArgs>(e => { tileDiscarded = e; }));
            mProcessor.Process(MjaiJson.Parse(dahai));
            mEventSender.ReceivedWithAnyArgs(1).OnTileDiscarded(null);
            Assert.IsFalse(mProcessor.IsRiichi);
            Assert.AreEqual(Player0, tileDiscarded.Player);
            Assert.AreEqual(Riichi(S7p), tileDiscarded.Discard);
        }

        [Test]
        public void TestReachAccepted()
        {
            const string message = @"{
                ""type"":""reach_accepted"",""actor"":1,""deltas"":[0,-1000,0,0],
                ""scores"":[28000,23000,24000,24000]
            }";

            mProcessor.Counters = 2;
            mProcessor.RiichiSticks = 3;

            ScoreUpdatedEventArgs scoreUpdated = null;
            mEventSender.OnScoreUpdated(
                Arg.Do<ScoreUpdatedEventArgs>(e => { scoreUpdated = e; }));
            SticksUpdatedEventArgs sticksUpdated = null;
            mEventSender.OnSticksUpdated(
                Arg.Do<SticksUpdatedEventArgs>(e => { sticksUpdated = e; }));
            RiichiEventArgs riichiAccepted = null;
            mEventSender.OnRiichiAccepted(
                Arg.Do<RiichiEventArgs>(e => { riichiAccepted = e; }));

            mProcessor.Process(MjaiJson.Parse(message));

            mEventSender.ReceivedWithAnyArgs(1).OnScoreUpdated(null);
            Assert.AreEqual(Player0, scoreUpdated.Player);
            Assert.AreEqual(23000, scoreUpdated.Score);
            Assert.AreEqual(-1000, scoreUpdated.Delta);

            mEventSender.ReceivedWithAnyArgs(1).OnSticksUpdated(null);
            Assert.AreEqual(2, sticksUpdated.Counters);
            Assert.AreEqual(4, sticksUpdated.RiichiSticks);
            Assert.AreEqual(2, mProcessor.Counters);
            Assert.AreEqual(4, mProcessor.RiichiSticks);

            mEventSender.ReceivedWithAnyArgs(1).OnRiichiAccepted(null);
            Assert.AreEqual(Player0, riichiAccepted.Player);
        }

        [Test]
        public void TestHoraTsumo()
        {
            const string message = @"{
                ""type"":""hora"",""actor"":2,""target"":2,""pai"":""2m"",
                ""uradora_markers"":[""8p""],
                ""hora_tehais"":[""1m"",""3m"",""5m"",""6m"",""7m"",""1p"",""2p"",""3p"",""4p"",
                                 ""5pr"",""6p"",""W"",""W"",""2m""],
                ""yakus"":[[""akadora"",1],[""reach"",1],[""menzenchin_tsumoho"",1]],
                ""fu"":30,""fan"":3,""hora_points"":4000,
                ""deltas"":[-2100,-1100,6300,-1100],""scores"":[25900,21900,29300,22900]
            }";

            WinningDeclaredEventArgs winningDeclared = null;
            mEventSender.OnWinningDeclared(
                Arg.Do<WinningDeclaredEventArgs>(e => { winningDeclared = e; }));
            PlayerHandUpdatedEventArgs playerHandUpdated = null;
            mEventSender.OnPlayerHandUpdated(
                Arg.Do<PlayerHandUpdatedEventArgs>(e => { playerHandUpdated = e; }));
            var scoreUpdates = new ScoreUpdatedEventArgs[4];
            mEventSender.OnScoreUpdated(
                Arg.Do<ScoreUpdatedEventArgs>(e => { scoreUpdates[e.Player.Id] = e; }));

            mProcessor.Process(MjaiJson.Parse(message));

            mEventSender.ReceivedWithAnyArgs(1).OnWinningDeclared(null);
            WinningInfo winningInfo = winningDeclared.WinningInfo;
            Assert.AreEqual(Player1, winningInfo.Winner);
            Assert.AreEqual(Player1, winningInfo.Feeder);
            Assert.AreEqual(30, winningInfo.Minipoints);
            Assert.AreEqual(3, winningInfo.Fan);
            Assert.AreEqual(4000, winningInfo.Points);
            Assert.AreEqual(1, winningDeclared.Uradora.Count);
            Assert.AreEqual(T8p, winningDeclared.Uradora[0].Indicator);
            Assert.AreEqual(T9, winningDeclared.Uradora[0].Tile);

            mEventSender.ReceivedWithAnyArgs(1).OnPlayerHandUpdated(null);
            Assert.AreEqual(new [] { W1p, W3p, W5p, W6p, W7p, T1p, T2p, T3p, T4p, T5r, T6p,
                                     FWp, FWp, Drawn(W2p) },
                            playerHandUpdated.Tiles);
            Assert.AreEqual(Player1, playerHandUpdated.Player);

            mEventSender.ReceivedWithAnyArgs(4).OnScoreUpdated(null);
            Assert.AreEqual(21900, scoreUpdates[0].Score);
            Assert.AreEqual(-1100, scoreUpdates[0].Delta);
            Assert.AreEqual(29300, scoreUpdates[1].Score);
            Assert.AreEqual(+6300, scoreUpdates[1].Delta);
            Assert.AreEqual(22900, scoreUpdates[2].Score);
            Assert.AreEqual(-1100, scoreUpdates[2].Delta);
            Assert.AreEqual(25900, scoreUpdates[3].Score);
            Assert.AreEqual(-2100, scoreUpdates[3].Delta);
        }

        [Test]
        public void TestHoraRon()
        {
            const string message = @"{
                ""type"":""hora"",""actor"":1,""target"":0,""pai"":""7s"",
                ""uradora_markers"":[""3s""],
                ""hora_tehais"":[""2m"",""3m"",""4m"",""4p"",""5pr"",""6p"",""6p"",""7p"",""8p"",
                                 ""6s"",""8s"",""N"",""N""],
                ""yakus"":[[""akadora"",1],[""reach"",1]],
                ""fu"":40,""fan"":2,""hora_points"":2600,
                ""deltas"":[-4400,8400,0,0],""scores"":[27500,22300,24300,25900]
            }";

            WinningDeclaredEventArgs winningDeclared = null;
            mEventSender.OnWinningDeclared(
                Arg.Do<WinningDeclaredEventArgs>(e => { winningDeclared = e; }));
            PlayerHandUpdatedEventArgs playerHandUpdated = null;
            mEventSender.OnPlayerHandUpdated(
                Arg.Do<PlayerHandUpdatedEventArgs>(e => { playerHandUpdated = e; }));
            var scoreUpdates = new ScoreUpdatedEventArgs[4];
            mEventSender.OnScoreUpdated(
                Arg.Do<ScoreUpdatedEventArgs>(e => { scoreUpdates[e.Player.Id] = e; }));

            mProcessor.Process(MjaiJson.Parse(message));

            mEventSender.ReceivedWithAnyArgs(1).OnWinningDeclared(null);
            WinningInfo winningInfo = winningDeclared.WinningInfo;
            Assert.AreEqual(Player0, winningInfo.Winner);
            Assert.AreEqual(Player3, winningInfo.Feeder);
            Assert.AreEqual(40, winningInfo.Minipoints);
            Assert.AreEqual(2, winningInfo.Fan);
            Assert.AreEqual(2600, winningInfo.Points);
            Assert.AreEqual(1, winningDeclared.Uradora.Count);
            Assert.AreEqual(S3p, winningDeclared.Uradora[0].Indicator);
            Assert.AreEqual(S4, winningDeclared.Uradora[0].Tile);

            mEventSender.ReceivedWithAnyArgs(1).OnPlayerHandUpdated(null);
            Assert.AreEqual(new [] { W2p, W3p, W4p, T4p, T5r, T6p, T6p, T7p, T8p, S6p, S8p,
                                     FNp, FNp, Claimed(S7p) },
                            playerHandUpdated.Tiles);
            Assert.AreEqual(Player0, playerHandUpdated.Player);

            mEventSender.ReceivedWithAnyArgs(2).OnScoreUpdated(null);
            Assert.AreEqual(22300, scoreUpdates[0].Score);
            Assert.AreEqual(+8400, scoreUpdates[0].Delta);
            Assert.AreEqual(27500, scoreUpdates[3].Score);
            Assert.AreEqual(-4400, scoreUpdates[3].Delta);
        }

        [Test]
        public void TestRyukyokuFanpai()
        {
            const string message = @"{
                ""type"":""ryukyoku"",""reason"":""fanpai"",
                ""tehais"":[
                    [""5m"",""5m"",""5mr"",""3s"",""3s"",""N"",""N""],
                    [""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?""],
                    [""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?""],
                    [""?"",""?"",""?"",""?""]
                ],
                ""tenpais"":[true,false,false,false],
                ""deltas"":[3000,-1000,-1000,-1000],""scores"":[28000,24000,24000,24000]
            }";

            HandDrawnEventArgs handDrawn = null;
            mEventSender.OnHandDrawn(
                Arg.Do<HandDrawnEventArgs>(e => { handDrawn = e; }));
            var tiles = new IPlayerHand[4];
            mEventSender.OnPlayerHandUpdated(
                Arg.Do<PlayerHandUpdatedEventArgs>(e => { tiles[e.Player.Id] = e.Tiles; }));
            var scoreUpdates = new ScoreUpdatedEventArgs[4];
            mEventSender.OnScoreUpdated(
                Arg.Do<ScoreUpdatedEventArgs>(e => { scoreUpdates[e.Player.Id] = e; }));

            mProcessor.Process(MjaiJson.Parse(message));

            mEventSender.ReceivedWithAnyArgs(1).OnHandDrawn(null);
            Assert.AreEqual(DrawHand.Exhaustive, handDrawn.Reason);

            mEventSender.ReceivedWithAnyArgs(1).OnPlayerHandUpdated(null);
            Assert.AreEqual(new [] { W5p, W5p, W5r, S3p, S3p, FNp, FNp }, tiles[3]);

            mEventSender.ReceivedWithAnyArgs(4).OnScoreUpdated(null);
            Assert.AreEqual(24000, scoreUpdates[0].Score);
            Assert.AreEqual(-1000, scoreUpdates[0].Delta);
            Assert.AreEqual(24000, scoreUpdates[1].Score);
            Assert.AreEqual(-1000, scoreUpdates[1].Delta);
            Assert.AreEqual(24000, scoreUpdates[2].Score);
            Assert.AreEqual(-1000, scoreUpdates[2].Delta);
            Assert.AreEqual(28000, scoreUpdates[3].Score);
            Assert.AreEqual(+3000, scoreUpdates[3].Delta);
        }

        [Test]
        public void TestRyukyokuKyushukyuhai()
        {
            const string message = @"{
                ""type"":""ryukyoku"",""reason"":""kyushukyuhai"",""actor"":1,
                ""tehais"":[
                    [""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?""],
                    [""1m"",""3m"",""5m"",""9m"",""9p"",""9p"",""1s"",""5sr"",""9s"",""E"",
                     ""S"",""P"",""C"",""7s""],
                    [""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?""],
                    [""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?"",""?""],
                ],
                ""tenpais"":[false,false,false,false],
                ""deltas"":[0,0,0,0],""scores"":[25000,25000,25000,25000]
            }";

            HandDrawnEventArgs handDrawn = null;
            mEventSender.OnHandDrawn(
                Arg.Do<HandDrawnEventArgs>(e => { handDrawn = e; }));
            var tiles = new IPlayerHand[4];
            mEventSender.OnPlayerHandUpdated(
                Arg.Do<PlayerHandUpdatedEventArgs>(e => { tiles[e.Player.Id] = e.Tiles; }));

            mProcessor.Process(MjaiJson.Parse(message));

            mEventSender.ReceivedWithAnyArgs(1).OnHandDrawn(null);
            Assert.AreEqual(DrawHand.NineTerminalsAndHonors, handDrawn.Reason);

            mEventSender.ReceivedWithAnyArgs(1).OnPlayerHandUpdated(null);
            Assert.AreEqual(new [] { W1p, W3p, W5p, W9p, T9p, T9p, S1p, S5r, S9p, FEp, FSp,
                                     JPp, JCp, S7p },
                            tiles[0]);

            mEventSender.ReceivedWithAnyArgs(0).OnScoreUpdated(null);
        }
    }
}
