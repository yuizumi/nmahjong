using System;
using NMahjong.Base;

namespace NMahjong.Japanese
{
    public partial class WinningInfo
    {
        private readonly PlayerId mWinner;
        private readonly PlayerId mFeeder;
        private readonly int? mPoints;
        private readonly int? mFan;
        private readonly int? mMinipoints;

        private WinningInfo(Builder builder)
        {
            mWinner = builder.mWinner.Value;
            mFeeder = (builder.mFeeder ?? builder.mWinner).Value;
            mPoints = builder.mPoints;
            mFan = builder.mFan;
            mMinipoints = builder.mMinipoints;
        }

        public PlayerId Winner
        {
            get { return mWinner; }
        }

        public PlayerId Feeder
        {
            get { return mFeeder; }
        }

        public Nullable<Int32> Points
        {
            get { return mPoints; }
        }

        public Nullable<Int32> Fan
        {
            get { return mFan; }
        }

        public Nullable<Int32> Minipoints
        {
            get { return mMinipoints; }
        }
    }
}
