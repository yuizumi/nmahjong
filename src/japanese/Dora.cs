using NMahjong.Aux;
using NMahjong.Base;

namespace NMahjong.Japanese
{
    public class Dora
    {
        private readonly CanonicalTile mIndicator;
        private readonly Tile mTile;

        private Dora(CanonicalTile indicator, Tile tile)
        {
            mIndicator = indicator;
            mTile = tile;
        }

        public CanonicalTile Indicator
        {
            get { return mIndicator; }
        }

        public Tile Tile
        {
            get { return mTile; }
        }

        public static Dora Next(CanonicalTile indicator)
        {
            CheckArg.NotNull(indicator, "indicator");
            return new Dora(indicator, DoraHelper.GetNext(indicator.BaseTile));
        }

        public static Dora Same(CanonicalTile indicator)
        {
            CheckArg.NotNull(indicator, "indicator");
            return new Dora(indicator, indicator.BaseTile);
        }

        // TODO(yuizumi): Add other static methods.
    }
}
