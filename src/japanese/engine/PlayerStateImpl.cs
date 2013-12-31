using System.Collections.Generic;
using System.Linq;
using NMahjong.Aux;
using NMahjong.Base;

namespace NMahjong.Japanese.Engine
{
    internal class PlayerStateImpl : IPlayerState
    {
        private readonly string mName;

        private Wind mSeatWind;
        private IPlayerHandInternal mTiles;
        private List<AnnotatedTile> mDiscards = null;
        private List<RevealedMeld> mMelds = null;

        internal PlayerStateImpl(string name, int score)
        {
            mName = name;
            Score = score;
        }

        public string Name
        {
            get { return mName; }
        }

        public int Score
        {
            get; internal set;
        }

        public Wind SeatWind
        {
            get { EnsureStarted(); return mSeatWind; }
        }

        public IPlayerHand Tiles  // Nullable.
        {
            get { EnsureStarted(); return mTiles; }
        }

        public ReadOnlyListView<AnnotatedTile> Discards
        {
            get { EnsureStarted(); return mDiscards.AsReadOnlyView(); }
        }

        public ReadOnlyListView<RevealedMeld> Melds
        {
            get { EnsureStarted(); return mMelds.AsReadOnlyView(); }
        }

        internal void StartHand(Wind seatWind)
        {
            mSeatWind = seatWind;
            mTiles = null;
            mDiscards = new List<AnnotatedTile>();
            mMelds = new List<RevealedMeld>();
        }

        internal void AnnotateLastDiscard(TileAnnotations annotations)
        {
            mDiscards[mDiscards.Count - 1] = mDiscards[mDiscards.Count - 1].With(annotations);
        }

        internal void Discard(AnnotatedTile tile)
        {
            EnsureTilesSet();
            mTiles.Discard(tile);
            mDiscards.Add(tile);
        }

        internal void Draw(CanonicalTile tile)
        {
            EnsureTilesSet();
            mTiles.Draw(tile);
        }

        internal void ExtendMeld(CanonicalTile tile)
        {
            EnsureTilesSet();
            int index = mMelds.FindIndex(m => m is OpenPung && m.Tiles[0] == tile.BaseTile);
            CheckArg.Expect(index >= 0, "tile", "No open pung matches with the specified tile.");
            // TODO(yuizumi): Consider a more efficient data structure.
            mTiles.Exclude(Enumerable.Repeat(tile, 1));
            mMelds[index] = ExtendedKong.Create(mMelds[index] as OpenPung, tile);
        }

        internal void RevealMeld(RevealedMeld meld)
        {
            EnsureTilesSet();
            mTiles.Exclude(meld.AnnotatedTiles.OfType<CanonicalTile>());
            mMelds.Add(meld);
        }

        internal void SetTiles(IPlayerHandInternal tiles)
        {
            mTiles = tiles;
        }

        [VisibleForTesting]
        internal void SetDiscardsForTest(params AnnotatedTile[] discards)
        {
            mDiscards = new List<AnnotatedTile>(discards);
        }

        [VisibleForTesting]
        internal void SetMeldsForTest(params RevealedMeld[] melds)
        {
            mMelds = new List<RevealedMeld>(melds);
        }

        private void EnsureStarted()
        {
            CheckState.Expect(mMelds != null, "No hands have been started yet.");
        }

        private void EnsureTilesSet()
        {
            CheckState.Expect(mTiles != null, "Tiles have not been set.");
        }
    }
}
