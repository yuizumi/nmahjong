using System;
using NMahjong.Aux;

namespace NMahjong.Japanese
{
    public class HandDrawnEventArgs : EventArgs
    {
        private readonly DrawHand mReason;

        public HandDrawnEventArgs(DrawHand reason)
        {
            CheckArg.Enum(reason, "reason");
            mReason = reason;
        }

        public DrawHand Reason
        {
            get { return mReason; }
        }
    }
}
