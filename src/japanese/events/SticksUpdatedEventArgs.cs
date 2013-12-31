using System;
using NMahjong.Aux;

namespace NMahjong.Japanese
{
    public class SticksUpdatedEventArgs : EventArgs
    {
        private readonly int mCounters;
        private readonly int mRiichiSticks;

        public SticksUpdatedEventArgs(int counters, int riichiSticks)
        {
            CheckArg.Minimum(counters, "counters", 0);
            mCounters = counters;
            CheckArg.Minimum(riichiSticks, "riichiSticks", 0);
            mRiichiSticks = riichiSticks;
        }

        public int Counters
        {
            get { return mCounters; }
        }

        public int RiichiSticks
        {
            get { return mRiichiSticks; }
        }
    }
}
