using System;
using NMahjong.Base;

namespace NMahjong.Japanese
{
    public class TileDrawnEventArgs : EventArgs
    {
        private readonly PlayerId mPlayer;
        private readonly CanonicalTile mTile;

        public TileDrawnEventArgs(PlayerId player, CanonicalTile tile)
        {
            mPlayer = player;
            mTile = tile;  // This parameter is allowed to be null.
        }

        public PlayerId Player
        {
            get { return mPlayer; }
        }

        public CanonicalTile Tile
        {
            get { return mTile; }
        }
    }
}
