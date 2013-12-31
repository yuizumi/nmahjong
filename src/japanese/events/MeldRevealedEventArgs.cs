using System;
using NMahjong.Aux;
using NMahjong.Base;

namespace NMahjong.Japanese
{
    public class MeldRevealedEventArgs : EventArgs
    {
        private readonly PlayerId mPlayer;
        private readonly RevealedMeld mMeld;

        public MeldRevealedEventArgs(PlayerId player, RevealedMeld meld)
        {
            CheckArg.NotNull(meld, "meld");
            CheckArg.Expect(!(meld is ExtendedKong),
                            "meld", "Argument must not be an extended meld.");
            mPlayer = player;
            mMeld = meld;
        }

        public PlayerId Player
        {
            get { return mPlayer; }
        }

        public RevealedMeld Meld
        {
            get { return mMeld; }
        }
    }
}
