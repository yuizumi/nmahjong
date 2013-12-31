using System;
using NMahjong.Base;

namespace NMahjong.Japanese
{
    public class ScoreUpdatedEventArgs : EventArgs
    {
        private readonly PlayerId mPlayer;
        private readonly int mScore;
        private readonly int mDelta;

        public ScoreUpdatedEventArgs(PlayerId player, int score, int delta)
        {
            mPlayer = player;
            mScore = score;
            mDelta = delta;
        }

        public PlayerId Player
        {
            get { return mPlayer; }
        }

        public int Score
        {
            get { return mScore; }
        }

        public int Delta
        {
            get { return mDelta; }
        }
    }
}
