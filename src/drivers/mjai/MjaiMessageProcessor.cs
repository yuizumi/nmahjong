using System;
using System.Collections.Generic;
using System.Linq;
using NMahjong.Aux;
using NMahjong.Base;
using NMahjong.Japanese;
using NMahjong.Japanese.Engine;

using TA = NMahjong.Japanese.TileAnnotations;

namespace NMahjong.Drivers.Mjai
{
    using OpenMeldCreateFunc = Func<IEnumerable<CanonicalTile>, CanonicalTile,
        PlayerId, RevealedMeld>;

    internal class MjaiMessageProcessor
    {
        private readonly IEventSender mEventSender;
        private readonly int mBaseId;

        private PlayerId? mReachActor;

        internal MjaiMessageProcessor(IEventSender eventSender, int baseId)
        {
            mEventSender = eventSender;
            mBaseId = baseId;
        }

        [VisibleForTesting]
        internal bool IsRiichi
        {
            get { return mReachActor.HasValue; }
        }

        [VisibleForTesting]
        internal int Counters
        {
            get; set;
        }

        [VisibleForTesting]
        internal int RiichiSticks
        {
            get; set;
        }

        internal void Process(MjaiJson json)
        {
            switch (json.Type) {
                case "start_kyoku":
                    ProcessStartKyoku(json);
                    break;
                case "end_kyoku":
                    ProcessEndKyoku(json);
                    break;
                case "tsumo":
                    ProcessTsumo(json);
                    break;
                case "dahai":
                    ProcessDahai(json);
                    break;
                case "pon":
                    ProcessPonChiKan(json, OpenPung.Create);
                    break;
                case "chi":
                    ProcessPonChiKan(json, OpenChow.Create);
                    break;
                case "daiminkan":
                    ProcessPonChiKan(json, OpenKong.Create);
                    break;
                case "ankan":
                    ProcessAnkan(json);
                    break;
                case "kakan":
                    ProcessKakan(json);
                    break;
                case "dora":
                    ProcessDora(json);
                    break;
                case "reach":
                    ProcessReach(json);
                    break;
                case "reach_accepted":
                    ProcessReachAccepted(json);
                    break;
                case "hora":
                    ProcessHora(json);
                    break;
                case "ryukyoku":
                    ProcessRyukyoku(json);
                    break;
                default:
                    throw new MjaiInvalidFieldException("type", json);
            }
        }

        private void ProcessStartKyoku(MjaiJson json)
        {
            mEventSender.OnHandStarting(new HandStartingEventArgs(
                Wall.Simple(), GetPlayerId(json, "oya"),
                json.Get<Wind>("bakaze"), json.Get<Int32>("kyoku")));

            Counters = json.Get<Int32>("honba");
            RiichiSticks= json.Get<Int32>("kyotaku");
            mEventSender.OnSticksUpdated(new SticksUpdatedEventArgs(Counters, RiichiSticks));

            var tehais = json.Get<CanonicalTile[][]>("tehais");
            for (int i = 0; i < 4; i++) {
                IPlayerHand playerHand = (IsTehaiHidden(tehais[i]))
                    ? PlayerHand.Hidden(tehais[i].Length)
                    : PlayerHand.Showed(tehais[i]);
                mEventSender.OnPlayerHandUpdated(
                    new PlayerHandUpdatedEventArgs(GetPlayerId(i), playerHand));
            }

            ProcessDoraMarker(json);

            mEventSender.OnHandStarted(EventArgs.Empty);
        }

        private void ProcessEndKyoku(MjaiJson json)
        {
            mEventSender.OnHandEnded(EventArgs.Empty);
        }

        private void ProcessTsumo(MjaiJson json)
        {
            mEventSender.OnTileDrawn(new TileDrawnEventArgs(
                GetPlayerId(json, "actor"), json.Get<CanonicalTile>("pai")));
        }

        private void ProcessDahai(MjaiJson json)
        {
            AnnotatedTile discard = json.Get<CanonicalTile>("pai")
                .With(IsRiichi ? TA.Riichi : TA.None)
                .With(json.Get<Boolean>("tsumogiri") ? TA.Drawn : TA.None);
            mEventSender.OnTileDiscarded(
                new TileDiscardedEventArgs(GetPlayerId(json, "actor"), discard));
            mReachActor = null;  // "reach" has now been fully processed.
        }

        private void ProcessPonChiKan(MjaiJson json, OpenMeldCreateFunc create)
        {
            RevealedMeld meld = create(json.Get<CanonicalTile[]>("consumed"),
                                       json.Get<CanonicalTile>("pai"),
                                       GetPlayerId(json, "target"));
            mEventSender.OnMeldRevealed(
                new MeldRevealedEventArgs(GetPlayerId(json, "actor"), meld));
        }

        private void ProcessAnkan(MjaiJson json)
        {
            var kong = ConcealedKong.Create(json.Get<CanonicalTile[]>("consumed"));
            mEventSender.OnMeldRevealed(new MeldRevealedEventArgs(
                GetPlayerId(json, "actor"), kong));
        }

        private void ProcessKakan(MjaiJson json)
        {
            mEventSender.OnMeldExtended(new MeldExtendedEventArgs(
                GetPlayerId(json, "actor"), json.Get<CanonicalTile>("pai")));
        }

        private void ProcessDora(MjaiJson json)
        {
            ProcessDoraMarker(json);
        }

        private void ProcessReach(MjaiJson json)
        {
            mEventSender.OnRiichiDeclared(new RiichiEventArgs(GetPlayerId(json, "actor")));
            mReachActor = GetPlayerId(json, "actor");
        }

        private void ProcessReachAccepted(MjaiJson json)
        {
            ProcessScores(json);
            ++RiichiSticks;
            mEventSender.OnSticksUpdated(new SticksUpdatedEventArgs(Counters, RiichiSticks));
            mEventSender.OnRiichiAccepted(new RiichiEventArgs(GetPlayerId(json, "actor")));
        }

        private void ProcessHora(MjaiJson json)
        {
            PlayerId winner = GetPlayerId(json, "actor");
            PlayerId feeder = GetPlayerId(json, "target");

            WinningInfo winningInfo = new WinningInfo.Builder()
                .SetWinner(winner).SetFeeder(feeder)
                .SetFan(json.Get<Int32>("fan"))
                .SetMinipoints(json.Get<Int32>("fu"))
                .SetPoints(json.Get<Int32>("hora_points")).Build();
            mEventSender.OnWinningDeclared(new WinningDeclaredEventArgs(
                winningInfo, json.Get<CanonicalTile[]>("uradora_markers").Select(Dora.Next)));

            Winning winning = (winner == feeder) ? Winning.SelfDraw : Winning.Discard;
            var tiles = json.Get<List<CanonicalTile>>("hora_tehais");
            if (winning == Winning.Discard) {
                tiles.Add(json.Get<CanonicalTile>("pai"));
            }
            mEventSender.OnPlayerHandUpdated(
                new PlayerHandUpdatedEventArgs(winner, PlayerHand.Winning(tiles, winning)));

            ProcessScores(json);
        }

        private void ProcessRyukyoku(MjaiJson json)
        {
            mEventSender.OnHandDrawn(new HandDrawnEventArgs(json.Get<DrawHand>("reason")));

            var tehais = json.Get<CanonicalTile[][]>("tehais");
            for (int i = 0; i < 4; i++) {
                if (IsTehaiHidden(tehais[i])) {
                    continue;
                }
                mEventSender.OnPlayerHandUpdated(
                    new PlayerHandUpdatedEventArgs(GetPlayerId(i), PlayerHand.Waiting(tehais[i])));
            }

            ProcessScores(json);
        }

        private void ProcessDoraMarker(MjaiJson json)
        {
            mEventSender.OnDoraAdded(
                new DoraAddedEventArgs(Dora.Next(json.Get<CanonicalTile>("dora_marker"))));
        }

        private void ProcessScores(MjaiJson json)
        {
            int[] scores = json.Get<Int32[]>("scores");
            int[] deltas = json.Get<Int32[]>("deltas");
            for (int i = 0; i < 4; i++) {
                if (deltas[i] == 0) {
                    continue;
                }
                mEventSender.OnScoreUpdated(
                    new ScoreUpdatedEventArgs(GetPlayerId(i), scores[i], deltas[i]));
            }
        }

        private PlayerId GetPlayerId(int mjaiId)
        {
            return new PlayerId((mjaiId - mBaseId + 4) % 4);
        }

        private PlayerId GetPlayerId(MjaiJson json, string field)
        {
            return new PlayerId((json.Get<Int32>(field) - mBaseId + 4) % 4);
        }

        private static bool IsTehaiHidden(IEnumerable<CanonicalTile> tehai)
        {
            if (tehai.All(tile => tile == null)) {
                return true;
            }
            if (tehai.All(tile => tile != null)) {
                return false;
            }
            throw new ArgumentException(
                "Tiles must be either all showed or all hidden.", "tehai");
        }
    }
}
