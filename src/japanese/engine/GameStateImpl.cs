using System;
using System.Collections.Generic;
using System.Linq;
using NMahjong.Aux;
using NMahjong.Base;

using TA = NMahjong.Japanese.TileAnnotations;

namespace NMahjong.Japanese.Engine
{
    internal class GameStateImpl : IGameState
    {
        private readonly Quad<PlayerStateImpl> mPlayerImpls;
        private readonly Quad<IPlayerState> mPlayers;

        private HandStartingEventArgs mHand;
        private List<Dora> mDora;
        private PlayerId mTurn;

        internal GameStateImpl(Quad<String> names, Quad<Int32> scores)
        {
            HandState = HandState.NotStarted;

            var players = new PlayerStateImpl[4];
            for (int i = 0; i < 4; i++) {
                players[i] = new PlayerStateImpl(names[i], scores[i]);
            }
            mPlayerImpls = Quad.Of(players);
            mPlayers = Quad.Of<IPlayerState>(players);
        }

        [VisibleForTesting]
        internal Quad<PlayerStateImpl> PlayerImpls
        {
            get { return mPlayerImpls; }
        }

        public Quad<IPlayerState> Players
        {
            get { return mPlayers; }
        }

        public HandState HandState
        {
            get; private set;
        }

        public PlayerId Turn
        {
            get { EnsureStarted(); return mTurn; }
        }

        public IWall Wall
        {
            get { EnsureStarted(); return mHand.Wall; }
        }

        public PlayerId Dealer
        {
            get { EnsureStarted(); return mHand.Dealer; }
        }

        public Wind PrevailingWind
        {
            get { EnsureStarted(); return mHand.PrevailingWind; }
        }

        public int HandNumber
        {
            get { EnsureStarted(); return mHand.HandNumber; }
        }

        public int Counters
        {
            get; private set;
        }

        public int RiichiSticks
        {
            get; private set;
        }

        public ReadOnlyListView<Dora> Dora
        {
            get { EnsureStarted(); return mDora.AsReadOnlyView(); }
        }

        internal void RegisterHandlers(IEventHandlerRegisterer registerer)
        {
            registerer.AddOnHandStarting(OnHandStarting);
            registerer.AddOnHandStarted(OnHandStarted);
            registerer.AddOnHandEnded(OnHandEnded);
            registerer.AddOnDoraAdded(OnDoraAdded);
            registerer.AddOnMeldExtended(OnMeldExtended);
            registerer.AddOnMeldRevealed(OnMeldRevealed);
            registerer.AddOnPlayerHandUpdated(OnPlayerHandUpdated);
            registerer.AddOnScoreUpdated(OnScoreUpdated);
            registerer.AddOnSticksUpdated(OnSticksUpdated);
            registerer.AddOnTileDiscarded(OnTileDiscarded);
            registerer.AddOnTileDrawn(OnTileDrawn);
        }

        [VisibleForTesting]
        internal void SetHandStateForTest(HandState handState)
        {
            HandState = handState;
        }

        private void OnHandStarting(object sender, HandStartingEventArgs e)
        {
            EnsureHandState(HandState.NotStarted, HandState.Ended);
            CheckArg.Expect(e.Wall is WallImpl, "e.Wall",
                            "Object must be created through {0}.", typeof(Wall));
            mDora = new List<Dora>();
            mTurn = e.Dealer;
            mHand = e;
            for (int i = 0; i < 4; i++) {
                mPlayerImpls[i].StartHand((Wind) ((i - e.Dealer.Id + 4) % 4));
            }
            HandState = HandState.Starting;
        }

        private void OnHandStarted(object sender, EventArgs e)
        {
            EnsureHandState(HandState.Starting);
            for (int i = 0; i < 4; i++) {
                CheckState.IsSet(mPlayerImpls[i].Tiles, "Players[" + i + "].Tiles");
            }
            HandState = HandState.Playing;
        }

        private void OnHandEnded(object sender, EventArgs e)
        {
            EnsureHandState(HandState.Playing);
            HandState = HandState.Ended;
        }

        private void OnDoraAdded(object sender, DoraAddedEventArgs e)
        {
            EnsureHandState(HandState.Starting, HandState.Playing);
            mDora.Add(e.Dora);
            (mHand.Wall as WallImpl).OnDoraAdded(e.Dora);
        }

        private void OnMeldExtended(object sender, MeldExtendedEventArgs e)
        {
            EnsureHandState(HandState.Playing);
            mTurn = e.Player;
            mPlayerImpls[e.Player].ExtendMeld(e.Extender);
        }

        private void OnMeldRevealed(object sender, MeldRevealedEventArgs e)
        {
            EnsureHandState(HandState.Playing);
            mTurn = e.Player;
            if (e.Meld.IsOpen()) {
                mPlayerImpls[e.Meld.Feeder].AnnotateLastDiscard(TA.Claimed);
            }
            mPlayerImpls[e.Player].RevealMeld(e.Meld);
        }

        private void OnPlayerHandUpdated(object sender, PlayerHandUpdatedEventArgs e)
        {
            EnsureHandState(HandState.Starting, HandState.Playing);
            CheckArg.Expect(e.Tiles is IPlayerHandInternal, "e.Tiles",
                            "Object must be created through {0}.", typeof(PlayerHand));
            mPlayerImpls[e.Player].SetTiles(e.Tiles as IPlayerHandInternal);
        }

        private void OnScoreUpdated(object sender, ScoreUpdatedEventArgs e)
        {
            EnsureHandState(HandState.Starting, HandState.Playing);
            mPlayerImpls[e.Player].Score = e.Score;
        }

        private void OnSticksUpdated(object sender, SticksUpdatedEventArgs e)
        {
            EnsureHandState(HandState.Starting, HandState.Playing);
            Counters = e.Counters;
            RiichiSticks = e.RiichiSticks;
        }

        private void OnTileDiscarded(object sender, TileDiscardedEventArgs e)
        {
            EnsureHandState(HandState.Playing);
            mTurn = e.Player;
            mPlayerImpls[e.Player].Discard(e.Discard);
        }

        private void OnTileDrawn(object sender, TileDrawnEventArgs e)
        {
            EnsureHandState(HandState.Playing);
            mTurn = e.Player;
            (mHand.Wall as WallImpl).OnTileDrawn(e.Tile);
            mPlayerImpls[e.Player].Draw(e.Tile);
        }

        private void EnsureStarted()
        {
            CheckState.Expect(HandState != HandState.NotStarted,
                              "No hands have been started yet.");
        }

        private void EnsureHandState(params HandState[] allowedStates)
        {
            CheckState.Expect(allowedStates.Contains(HandState),
                              "Event is inconsistent with HandState.");
        }
    }
}
