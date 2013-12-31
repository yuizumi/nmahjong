using System;
using NMahjong.Aux;
using NMahjong.Base;

namespace NMahjong.Japanese
{
    public class HandStartingEventArgs : EventArgs
    {
        private readonly IWall mWall;
        private readonly PlayerId mDealer;
        private readonly Wind mPrevailingWind;
        private readonly int mHandNumber;

        public HandStartingEventArgs(IWall wall, PlayerId dealer,
                                     Wind prevailingWind, int handNumber)
        {
            CheckArg.NotNull(wall, "wall");
            mWall = wall;

            mDealer = dealer;

            CheckArg.Enum(prevailingWind, "prevailingWind");
            mPrevailingWind = prevailingWind;

            CheckArg.Range (handNumber, "handNumber", 1, 4);
            mHandNumber = handNumber;
        }

        public IWall Wall
        {
            get { return mWall; }
        }

        public PlayerId Dealer
        {
            get { return mDealer; }
        }

        public Wind PrevailingWind
        {
            get { return mPrevailingWind; }
        }

        public int HandNumber
        {
            get { return mHandNumber; }
        }
    }
}
