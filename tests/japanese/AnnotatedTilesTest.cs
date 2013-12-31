using NUnit.Framework;
using NMahjong.Testing;

using TA = NMahjong.Japanese.TileAnnotations;

namespace NMahjong.Japanese
{
    [TestFixture]
    public class AnnotatedTilesTest : TestHelperWithAnnotatedTiles
    {
        [Test, TestCaseSource("TestHasSource")]
        public void TestHas(AnnotatedTile annotatedTile,
                            TileAnnotations annotations,
                            bool expected)
        {
            Assert.AreEqual(expected, annotatedTile.Has(annotations));
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestHasSource = {
            Data(T5p, TA.Red, false),
            Data(T5r, TA.Red, true),
            Data(T5r, TA.Drawn, false),
            Data(AnnotatedTile.Of(T5, TA.Red | TA.Drawn), TA.Red, true),
            Data(AnnotatedTile.Of(T5, TA.Red | TA.Drawn), TA.Drawn, true),
            Data(AnnotatedTile.Of(T5, TA.Red | TA.Drawn), TA.Claimed, false),
            Data(T5p, TA.Red | TA.Claimed, false),
            Data(T5r, TA.Red | TA.Claimed, true),
        };
        #pragma warning restore 414

        [Test, TestCaseSource("TestStripAnnotationsSource")]
        public void TestStripAnnotations(AnnotatedTile annotatedTile,
                                         CanonicalTile expected)
        {
            Assert.AreEqual(expected, annotatedTile.StripAnnotations());
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestStripAnnotationsSource = {
            Data(T5p, T5p),
            Data(T5r, T5r),
            Data(AnnotatedTile.Of(T5, TA.Claimed), T5p),
            Data(AnnotatedTile.Of(T5, TA.Claimed | TA.Riichi), T5p),
            Data(AnnotatedTile.Of(T5, TA.Claimed | TA.Red), T5r),
        };
        #pragma warning restore 414

        [Test, TestCaseSource("TestWithSource")]
        public void TestWith(AnnotatedTile annotatedTile,
                             TileAnnotations annotations,
                             AnnotatedTile expected)
        {
            Assert.AreEqual(expected, annotatedTile.With(annotations));
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestWithSource = {
            Data(T5p, TA.Red, T5r),
            Data(T5r, TA.Red, T5r),
            Data(T5r, TA.Claimed, AnnotatedTile.Of(T5, TA.Red | TA.Claimed)),
        };
        #pragma warning restore 414

        [Test, TestCaseSource("TestWithoutSource")]
        public void TestWithout(AnnotatedTile annotatedTile,
                                TileAnnotations annotations,
                                AnnotatedTile expected)
        {
            Assert.AreEqual(expected, annotatedTile.Without(annotations));
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestWithoutSource = {
            Data(T5r, TA.Red, T5p),
            Data(T5p, TA.Red, T5p),
            Data(AnnotatedTile.Of(T5, TA.Red | TA.Claimed), TA.Claimed, T5r),
        };
        #pragma warning restore 414
    }
}
