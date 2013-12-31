using System;
using NMahjong.Aux;
using NMahjong.Base;

namespace NMahjong.Japanese
{
    public class PlayerHandUpdatedEventArgs : EventArgs
    {
        private readonly PlayerId mPlayer;
        private readonly IPlayerHand mTiles;

        public PlayerHandUpdatedEventArgs(PlayerId player, IPlayerHand tiles)
        {
            CheckArg.NotNull(tiles, "tiles");
            mPlayer = player;
            mTiles = tiles;
        }

        public PlayerId Player
        {
            get { return mPlayer; }
        }

        public IPlayerHand Tiles
        {
            get { return mTiles; }
        }
    }
}
