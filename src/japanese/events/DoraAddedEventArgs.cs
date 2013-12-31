using System;
using NMahjong.Aux;

namespace NMahjong.Japanese
{
    public class DoraAddedEventArgs : EventArgs
    {
        private readonly Dora mDora;

        public DoraAddedEventArgs(Dora dora)
        {
            CheckArg.NotNull(dora, "dora");
            mDora = dora;
        }

        public Dora Dora
        {
            get { return mDora; }
        }
    }
}
