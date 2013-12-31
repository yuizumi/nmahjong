using NMahjong.Aux;
using NMahjong.Base;

namespace NMahjong.Japanese
{
    public partial class WinningInfo
    {
        public class Builder
        {
            internal PlayerId? mWinner;
            internal PlayerId? mFeeder;
            internal int? mPoints;
            internal int? mFan;
            internal int? mMinipoints;

            public Builder()
            {
            }

            public WinningInfo Build()
            {
                CheckState.IsSet(mWinner, "Winner");
                return new WinningInfo(this);
            }

            public Builder SetWinner(PlayerId value)
            {
                mWinner = value;
                return this;
            }

            public Builder SetFeeder(PlayerId value)
            {
                mFeeder = value;
                return this;
            }

            public Builder SetPoints(int value)
            {
                CheckArg.Minimum(value, "value", 0);
                mPoints = value;
                return this;
            }

            public Builder SetFan(int value)
            {
                CheckArg.Minimum(value, "value", 0);
                mFan = value;
                return this;
            }

            public Builder SetMinipoints(int value)
            {
                CheckArg.Minimum(value, "value", 0);
                mMinipoints = value;
                return this;
            }
        }
    }
}
