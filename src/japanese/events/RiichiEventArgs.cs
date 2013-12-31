using System;
using NMahjong.Base;

namespace NMahjong.Japanese
{
    public class RiichiEventArgs : EventArgs
    {
        private readonly PlayerId mPlayer;

        public RiichiEventArgs(PlayerId player)
        {
            mPlayer = player;
        }

        public PlayerId Player
        {
            get { return mPlayer; }
        }
    }
}
