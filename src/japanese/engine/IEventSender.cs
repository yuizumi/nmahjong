using System;

namespace NMahjong.Japanese.Engine
{
    public interface IEventSender
    {
        void OnDoraAdded(DoraAddedEventArgs e);
        void OnGameEnded(EventArgs e);
        void OnGameStarted(EventArgs e);
        void OnHandDrawn(HandDrawnEventArgs e);
        void OnHandEnded(EventArgs e);
        void OnHandStarted(EventArgs e);
        void OnHandStarting(HandStartingEventArgs e);
        void OnMeldExtended(MeldExtendedEventArgs e);
        void OnMeldRevealed(MeldRevealedEventArgs e);
        void OnPlayerHandUpdated(PlayerHandUpdatedEventArgs e);
        void OnRiichiAccepted(RiichiEventArgs e);
        void OnRiichiDeclared(RiichiEventArgs e);
        void OnScoreUpdated(ScoreUpdatedEventArgs e);
        void OnSticksUpdated(SticksUpdatedEventArgs e);
        void OnTileDiscarded(TileDiscardedEventArgs e);
        void OnTileDrawn(TileDrawnEventArgs e);
        void OnWinningDeclared(WinningDeclaredEventArgs e);
    }
}
