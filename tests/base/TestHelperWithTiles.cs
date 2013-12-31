using NUnit.Framework;
using NMahjong.Base;

namespace NMahjong.Testing
{
    public class TestHelperWithTiles : TestHelper
    {
        protected static readonly Tile T1 = Tile.T1;
        protected static readonly Tile T2 = Tile.T2;
        protected static readonly Tile T3 = Tile.T3;
        protected static readonly Tile T4 = Tile.T4;
        protected static readonly Tile T5 = Tile.T5;
        protected static readonly Tile T6 = Tile.T6;
        protected static readonly Tile T7 = Tile.T7;
        protected static readonly Tile T8 = Tile.T8;
        protected static readonly Tile T9 = Tile.T9;
        protected static readonly Tile S1 = Tile.S1;
        protected static readonly Tile S2 = Tile.S2;
        protected static readonly Tile S3 = Tile.S3;
        protected static readonly Tile S4 = Tile.S4;
        protected static readonly Tile S5 = Tile.S5;
        protected static readonly Tile S6 = Tile.S6;
        protected static readonly Tile S7 = Tile.S7;
        protected static readonly Tile S8 = Tile.S8;
        protected static readonly Tile S9 = Tile.S9;
        protected static readonly Tile W1 = Tile.W1;
        protected static readonly Tile W2 = Tile.W2;
        protected static readonly Tile W3 = Tile.W3;
        protected static readonly Tile W4 = Tile.W4;
        protected static readonly Tile W5 = Tile.W5;
        protected static readonly Tile W6 = Tile.W6;
        protected static readonly Tile W7 = Tile.W7;
        protected static readonly Tile W8 = Tile.W8;
        protected static readonly Tile W9 = Tile.W9;
        protected static readonly Tile FE = Tile.FE;
        protected static readonly Tile FS = Tile.FS;
        protected static readonly Tile FW = Tile.FW;
        protected static readonly Tile FN = Tile.FN;
        protected static readonly Tile JP = Tile.JP;
        protected static readonly Tile JF = Tile.JF;
        protected static readonly Tile JC = Tile.JC;

        protected const Suit Dots = Suit.Dots;
        protected const Suit Bams = Suit.Bams;
        protected const Suit Craks = Suit.Craks;
        protected const Suit Winds = Suit.Winds;
        protected const Suit Dragons = Suit.Dragons;

        protected static readonly PlayerId Player0 = new PlayerId(0);
        protected static readonly PlayerId Player1 = new PlayerId(1);
        protected static readonly PlayerId Player2 = new PlayerId(2);
        protected static readonly PlayerId Player3 = new PlayerId(3);
    }
}
