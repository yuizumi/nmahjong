using NUnit.Framework;
using NMahjong.Base;
using NMahjong.Testing;

using TA = NMahjong.Japanese.TileAnnotations;

namespace NMahjong.Japanese
{
    [TestFixture]
    public class AnnotatedTileTest : TestHelperWithTiles
    {
        private static AnnotatedTile AT(Tile baseTile,
                                        TileAnnotations annotations)
        {
            return new AnnotatedTile(baseTile, annotations);
        }

        [Test, TestCaseSource("TestEqualsSource")]
        public void TestEquals(object x, object y, bool expected)
        {
            Assert.AreEqual(expected, x.Equals(y));
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestEqualsSource = {
            Data(AT(T5, TA.None), AT(T5, TA.None), true ),
            Data(AT(T5, TA.Red ), AT(T5, TA.Red ), true ),
            Data(AT(T5, TA.Red ), AT(T5, TA.None), false),
            Data(AT(T5, TA.None), AT(Tile.S5, TA.None), false),
            Data(AT(T5, TA.Red ), AT(Tile.S5, TA.None), false),
            Data(AT(T5, TA.None), T5, false),
            Data(AT(T5, TA.None), null, false),
        };
        #pragma warning restore 414

        [Test, TestCaseSource("TestToStringSource")]
        public void TestToString(AnnotatedTile at, string expected)
        {
            Assert.AreEqual(expected, at.ToString());
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestToStringSource = {
            Data(AT(T5, TA.None), "T5p"),
            Data(AT(T5, TA.Red ), "T5r"),
            Data(AT(T5, TA.Claimed), "T5p<Claimed>"),
            Data(AT(T5, TA.Drawn | TA.Claimed), "T5p<Drawn, Claimed>"),
            Data(AT(T5, TA.Red | TA.Claimed), "T5r<Claimed>"),
        };
        #pragma warning restore 414
    }
}
