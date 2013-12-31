using System;

namespace NMahjong.Japanese
{
    public interface IEventHandlerRegisterer
    {
        void AddOnDoraAdded(EventHandler<DoraAddedEventArgs> handler);
        void AddOnGameEnded(EventHandler<EventArgs> handler);
        void AddOnGameStarted(EventHandler<EventArgs> handler);
        void AddOnHandDrawn(EventHandler<HandDrawnEventArgs> handler);
        void AddOnHandEnded(EventHandler<EventArgs> handler);
        void AddOnHandStarted(EventHandler<EventArgs> handler);
        void AddOnHandStarting(EventHandler<HandStartingEventArgs> handler);
        void AddOnMeldExtended(EventHandler<MeldExtendedEventArgs> handler);
        void AddOnMeldRevealed(EventHandler<MeldRevealedEventArgs> handler);
        void AddOnPlayerHandUpdated(EventHandler<PlayerHandUpdatedEventArgs> handler);
        void AddOnRiichiAccepted(EventHandler<RiichiEventArgs> handler);
        void AddOnRiichiDeclared(EventHandler<RiichiEventArgs> handler);
        void AddOnScoreUpdated(EventHandler<ScoreUpdatedEventArgs> handler);
        void AddOnSticksUpdated(EventHandler<SticksUpdatedEventArgs> handler);
        void AddOnTileDiscarded(EventHandler<TileDiscardedEventArgs> handler);
        void AddOnTileDrawn(EventHandler<TileDrawnEventArgs> handler);
        void AddOnWinningDeclared(EventHandler<WinningDeclaredEventArgs> handler);
    }
}
