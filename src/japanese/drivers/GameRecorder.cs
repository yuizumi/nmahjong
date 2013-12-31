using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using NMahjong.Base;

namespace NMahjong.Japanese.Drivers
{
    public class GameRecorder
    {
        private static readonly JsonSerializer Serializer = CreateSerializer();

        private readonly IGameState mGameState;
        private readonly TextWriter mWriter;

        private string mSeparator = "";

        public GameRecorder(IGameState gameState, TextWriter writer)
        {
            mGameState = gameState;
            mWriter = writer;
        }

        private static JsonSerializer CreateSerializer()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(TileConverter.Converter);
            settings.Converters.Add(AnnotatedTileConverter.Converter);
            settings.Converters.Add(RevealedMeldConverter.Converter);
            return JsonSerializer.Create(settings);
        }

        public void RegisterHandlers(IEventHandlerRegisterer registerer)
        {
            registerer.AddOnDoraAdded(OnDoraAdded);
            registerer.AddOnGameEnded(OnGameEnded);
            registerer.AddOnGameStarted(OnGameStarted);
            registerer.AddOnHandDrawn(OnHandDrawn);
            registerer.AddOnHandEnded(OnHandEnded);
            registerer.AddOnHandStarted(OnHandStarted);
            registerer.AddOnHandStarting(OnHandStarting);
            registerer.AddOnMeldExtended(OnMeldExtended);
            registerer.AddOnMeldRevealed(OnMeldRevealed);
            registerer.AddOnPlayerHandUpdated(OnPlayerHandUpdated);
            registerer.AddOnRiichiAccepted(OnRiichiAccepted);
            registerer.AddOnRiichiDeclared(OnRiichiDeclared);
            registerer.AddOnScoreUpdated(OnScoreUpdated);
            registerer.AddOnSticksUpdated(OnSticksUpdated);
            registerer.AddOnTileDiscarded(OnTileDiscarded);
            registerer.AddOnTileDrawn(OnTileDrawn);
            registerer.AddOnWinningDeclared(OnWinningDeclared);
        }

        private void WriteRecord(string eventName, EventArgs eventArgs)
        {
            mWriter.Write(mSeparator);
            Serializer.Serialize(
                mWriter, new { @event = eventName, args = eventArgs, state = mGameState });
            mSeparator = ",";
        }

        private void OnGameStarted(object sender, EventArgs e)
        {
            mWriter.Write("[");
            mSeparator = "";
        }

        private void OnGameEnded(object sender, EventArgs e)
        {
            mWriter.Write("]");
        }

        private void OnDoraAdded(object sender, DoraAddedEventArgs e)
        {
            WriteRecord("DoraAdded", e);
        }

        private void OnHandDrawn(object sender, HandDrawnEventArgs e)
        {
            WriteRecord("HandDrawn", e);
        }

        private void OnHandEnded(object sender, EventArgs e)
        {
            WriteRecord("HandEnded", e);
        }

        private void OnHandStarted(object sender, EventArgs e)
        {
            WriteRecord("HandStarted", e);
        }

        private void OnHandStarting(object sender, HandStartingEventArgs e)
        {
            WriteRecord("HandStarting", e);
        }

        private void OnMeldExtended(object sender, MeldExtendedEventArgs e)
        {
            WriteRecord("MeldExtended", e);
        }

        private void OnMeldRevealed(object sender, MeldRevealedEventArgs e)
        {
            WriteRecord("MeldRevealed", e);
        }

        private void OnPlayerHandUpdated(object sender, PlayerHandUpdatedEventArgs e)
        {
            WriteRecord("PlayerHandUpdated", e);
        }

        private void OnRiichiAccepted(object sender, RiichiEventArgs e)
        {
            WriteRecord("RiichiAccepted", e);
        }

        private void OnRiichiDeclared(object sender, RiichiEventArgs e)
        {
            WriteRecord("RiichiDeclared", e);
        }

        private void OnScoreUpdated(object sender, ScoreUpdatedEventArgs e)
        {
            WriteRecord("ScoreUpdated", e);
        }

        private void OnSticksUpdated(object sender, SticksUpdatedEventArgs e)
        {
            WriteRecord("SticksUpdated", e);
        }

        private void OnTileDiscarded(object sender, TileDiscardedEventArgs e)
        {
            WriteRecord("TileDiscarded", e);
        }

        private void OnTileDrawn(object sender, TileDrawnEventArgs e)
        {
            WriteRecord("TileDrawn", e);
        }

        private void OnWinningDeclared(object sender, WinningDeclaredEventArgs e)
        {
            WriteRecord("WinningDeclared", e);
        }
    }
}
