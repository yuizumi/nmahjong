using System;

namespace NMahjong.Japanese.Engine
{
    public class EventSender : IEventSender, IEventHandlerRegisterer
    {
        private event EventHandler<DoraAddedEventArgs> DoraAdded;
        private event EventHandler<EventArgs> GameEnded;
        private event EventHandler<EventArgs> GameStarted;
        private event EventHandler<HandDrawnEventArgs> HandDrawn;
        private event EventHandler<EventArgs> HandEnded;
        private event EventHandler<EventArgs> HandStarted;
        private event EventHandler<HandStartingEventArgs> HandStarting;
        private event EventHandler<MeldExtendedEventArgs> MeldExtended;
        private event EventHandler<MeldRevealedEventArgs> MeldRevealed;
        private event EventHandler<PlayerHandUpdatedEventArgs> PlayerHandUpdated;
        private event EventHandler<RiichiEventArgs> RiichiAccepted;
        private event EventHandler<RiichiEventArgs> RiichiDeclared;
        private event EventHandler<ScoreUpdatedEventArgs> ScoreUpdated;
        private event EventHandler<SticksUpdatedEventArgs> SticksUpdated;
        private event EventHandler<TileDiscardedEventArgs> TileDiscarded;
        private event EventHandler<TileDrawnEventArgs> TileDrawn;
        private event EventHandler<WinningDeclaredEventArgs> WinningDeclared;

        public void OnDoraAdded(DoraAddedEventArgs e)
        {
            if (DoraAdded != null) DoraAdded(this, e);
        }

        public void OnGameEnded(EventArgs e)
        {
            if (GameEnded != null) GameEnded(this, e);
        }

        public void OnGameStarted(EventArgs e)
        {
            if (GameStarted != null) GameStarted(this, e);
        }

        public void OnHandDrawn(HandDrawnEventArgs e)
        {
            if (HandDrawn != null) HandDrawn(this, e);
        }

        public void OnHandEnded(EventArgs e)
        {
            if (HandEnded != null) HandEnded(this, e);
        }

        public void OnHandStarted(EventArgs e)
        {
            if (HandStarted != null) HandStarted(this, e);
        }

        public void OnHandStarting(HandStartingEventArgs e)
        {
            if (HandStarting != null) HandStarting(this, e);
        }

        public void OnMeldExtended(MeldExtendedEventArgs e)
        {
            if (MeldExtended != null) MeldExtended(this, e);
        }

        public void OnMeldRevealed(MeldRevealedEventArgs e)
        {
            if (MeldRevealed != null) MeldRevealed(this, e);
        }

        public void OnPlayerHandUpdated(PlayerHandUpdatedEventArgs e)
        {
            if (PlayerHandUpdated != null) PlayerHandUpdated(this, e);
        }

        public void OnRiichiAccepted(RiichiEventArgs e)
        {
            if (RiichiAccepted != null) RiichiAccepted(this, e);
        }

        public void OnRiichiDeclared(RiichiEventArgs e)
        {
            if (RiichiDeclared != null) RiichiDeclared(this, e);
        }

        public void OnScoreUpdated(ScoreUpdatedEventArgs e)
        {
            if (ScoreUpdated != null) ScoreUpdated(this, e);
        }

        public void OnSticksUpdated(SticksUpdatedEventArgs e)
        {
            if (SticksUpdated != null) SticksUpdated(this, e);
        }

        public void OnTileDiscarded(TileDiscardedEventArgs e)
        {
            if (TileDiscarded != null) TileDiscarded(this, e);
        }

        public void OnTileDrawn(TileDrawnEventArgs e)
        {
            if (TileDrawn != null) TileDrawn(this, e);
        }

        public void OnWinningDeclared(WinningDeclaredEventArgs e)
        {
            if (WinningDeclared != null) WinningDeclared(this, e);
        }

        public void AddOnDoraAdded(EventHandler<DoraAddedEventArgs> handler)
        {
            DoraAdded += handler;
        }

        public void AddOnGameEnded(EventHandler<EventArgs> handler)
        {
            GameEnded += handler;
        }

        public void AddOnGameStarted(EventHandler<EventArgs> handler)
        {
            GameStarted += handler;
        }

        public void AddOnHandDrawn(EventHandler<HandDrawnEventArgs> handler)
        {
            HandDrawn += handler;
        }

        public void AddOnHandEnded(EventHandler<EventArgs> handler)
        {
            HandEnded += handler;
        }

        public void AddOnHandStarted(EventHandler<EventArgs> handler)
        {
            HandStarted += handler;
        }

        public void AddOnHandStarting(EventHandler<HandStartingEventArgs> handler)
        {
            HandStarting += handler;
        }

        public void AddOnMeldExtended(EventHandler<MeldExtendedEventArgs> handler)
        {
            MeldExtended += handler;
        }

        public void AddOnMeldRevealed(EventHandler<MeldRevealedEventArgs> handler)
        {
            MeldRevealed += handler;
        }

        public void AddOnPlayerHandUpdated(EventHandler<PlayerHandUpdatedEventArgs> handler)
        {
            PlayerHandUpdated += handler;
        }

        public void AddOnRiichiAccepted(EventHandler<RiichiEventArgs> handler)
        {
            RiichiAccepted += handler;
        }

        public void AddOnRiichiDeclared(EventHandler<RiichiEventArgs> handler)
        {
            RiichiDeclared += handler;
        }

        public void AddOnScoreUpdated(EventHandler<ScoreUpdatedEventArgs> handler)
        {
            ScoreUpdated += handler;
        }

        public void AddOnSticksUpdated(EventHandler<SticksUpdatedEventArgs> handler)
        {
            SticksUpdated += handler;
        }

        public void AddOnTileDiscarded(EventHandler<TileDiscardedEventArgs> handler)
        {
            TileDiscarded += handler;
        }

        public void AddOnTileDrawn(EventHandler<TileDrawnEventArgs> handler)
        {
            TileDrawn += handler;
        }

        public void AddOnWinningDeclared(EventHandler<WinningDeclaredEventArgs> handler)
        {
            WinningDeclared += handler;
        }
    }
}
