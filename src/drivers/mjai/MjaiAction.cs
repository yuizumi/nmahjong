using NMahjong.Base;
using NMahjong.Japanese;

namespace NMahjong.Drivers.Mjai
{
    internal class MjaiAction
    {
        private readonly IPlayerAction mAction;
        private readonly PlayerId mTarget;
        private readonly CanonicalTile mPlayedTile;

        internal MjaiAction(IPlayerAction action, PlayerId target,
                            CanonicalTile playedTile)
        {
            mAction = action;
            mTarget = target;
            mPlayedTile = playedTile;
        }

        internal IPlayerAction Action
        {
            get { return mAction; }
        }

        internal PlayerId Target
        {
            get { return mTarget; }
        }

        internal CanonicalTile PlayedTile
        {
            get { return mPlayedTile; }
        }
    }
}
