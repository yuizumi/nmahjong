using NMahjong.Aux;

namespace NMahjong.Japanese
{
    public abstract class MeldAction
    {
        private readonly RevealedMeld mMeld;

        internal MeldAction(RevealedMeld meld)
        {
            CheckArg.NotNull(meld, "meld");
            mMeld = meld;
        }

        public RevealedMeld Meld
        {
            get { return mMeld; }
        }
    }
}
