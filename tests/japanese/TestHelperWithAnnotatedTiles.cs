using NUnit.Framework;

using NMahjong.Base;
using NMahjong.Japanese;

using TA = NMahjong.Japanese.TileAnnotations;

namespace NMahjong.Testing
{
    public class TestHelperWithAnnotatedTiles : TestHelperWithTiles
    {
        protected static readonly CanonicalTile T1p = CanonicalTile.Plain(Tile.T1);
        protected static readonly CanonicalTile T2p = CanonicalTile.Plain(Tile.T2);
        protected static readonly CanonicalTile T3p = CanonicalTile.Plain(Tile.T3);
        protected static readonly CanonicalTile T4p = CanonicalTile.Plain(Tile.T4);
        protected static readonly CanonicalTile T5p = CanonicalTile.Plain(Tile.T5);
        protected static readonly CanonicalTile T6p = CanonicalTile.Plain(Tile.T6);
        protected static readonly CanonicalTile T7p = CanonicalTile.Plain(Tile.T7);
        protected static readonly CanonicalTile T8p = CanonicalTile.Plain(Tile.T8);
        protected static readonly CanonicalTile T9p = CanonicalTile.Plain(Tile.T9);
        protected static readonly CanonicalTile S1p = CanonicalTile.Plain(Tile.S1);
        protected static readonly CanonicalTile S2p = CanonicalTile.Plain(Tile.S2);
        protected static readonly CanonicalTile S3p = CanonicalTile.Plain(Tile.S3);
        protected static readonly CanonicalTile S4p = CanonicalTile.Plain(Tile.S4);
        protected static readonly CanonicalTile S5p = CanonicalTile.Plain(Tile.S5);
        protected static readonly CanonicalTile S6p = CanonicalTile.Plain(Tile.S6);
        protected static readonly CanonicalTile S7p = CanonicalTile.Plain(Tile.S7);
        protected static readonly CanonicalTile S8p = CanonicalTile.Plain(Tile.S8);
        protected static readonly CanonicalTile S9p = CanonicalTile.Plain(Tile.S9);
        protected static readonly CanonicalTile W1p = CanonicalTile.Plain(Tile.W1);
        protected static readonly CanonicalTile W2p = CanonicalTile.Plain(Tile.W2);
        protected static readonly CanonicalTile W3p = CanonicalTile.Plain(Tile.W3);
        protected static readonly CanonicalTile W4p = CanonicalTile.Plain(Tile.W4);
        protected static readonly CanonicalTile W5p = CanonicalTile.Plain(Tile.W5);
        protected static readonly CanonicalTile W6p = CanonicalTile.Plain(Tile.W6);
        protected static readonly CanonicalTile W7p = CanonicalTile.Plain(Tile.W7);
        protected static readonly CanonicalTile W8p = CanonicalTile.Plain(Tile.W8);
        protected static readonly CanonicalTile W9p = CanonicalTile.Plain(Tile.W9);
        protected static readonly CanonicalTile FEp = CanonicalTile.Plain(Tile.FE);
        protected static readonly CanonicalTile FSp = CanonicalTile.Plain(Tile.FS);
        protected static readonly CanonicalTile FWp = CanonicalTile.Plain(Tile.FW);
        protected static readonly CanonicalTile FNp = CanonicalTile.Plain(Tile.FN);
        protected static readonly CanonicalTile JPp = CanonicalTile.Plain(Tile.JP);
        protected static readonly CanonicalTile JFp = CanonicalTile.Plain(Tile.JF);
        protected static readonly CanonicalTile JCp = CanonicalTile.Plain(Tile.JC);

        protected static readonly CanonicalTile T5r = CanonicalTile.Red(Tile.T5);
        protected static readonly CanonicalTile S5r = CanonicalTile.Red(Tile.S5);
        protected static readonly CanonicalTile W5r = CanonicalTile.Red(Tile.W5);

        protected static AnnotatedTile Drawn(AnnotatedTile tile)
        {
            return AnnotatedTile.Of(tile.BaseTile, tile.Annotations | TA.Drawn);
        }

        protected static AnnotatedTile Claimed(AnnotatedTile tile)
        {
            return AnnotatedTile.Of(tile.BaseTile, tile.Annotations | TA.Claimed);
        }

        protected static AnnotatedTile Extending(AnnotatedTile tile)
        {
            return AnnotatedTile.Of(tile.BaseTile, tile.Annotations | TA.Extending);
        }

        protected static AnnotatedTile Riichi(AnnotatedTile tile)
        {
            return AnnotatedTile.Of(tile.BaseTile, tile.Annotations | TA.Riichi);
        }
    }
}
