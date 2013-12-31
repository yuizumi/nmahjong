using System;
using NMahjong.Aux;
using NMahjong.Base;

namespace NMahjong.Japanese
{
    public class MeldExtendedEventArgs : EventArgs
    {
        private readonly PlayerId mPlayer;
        private readonly CanonicalTile mExtender;

        public MeldExtendedEventArgs(PlayerId player, CanonicalTile extender)
        {
            CheckArg.NotNull(extender, "extender");
            mPlayer = player;
            mExtender = extender;
        }

        public PlayerId Player
        {
            get { return mPlayer; }
        }

        public CanonicalTile Extender
        {
            get { return mExtender; }
        }
    }
}
