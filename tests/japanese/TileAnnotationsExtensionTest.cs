using NUnit.Framework;
using NMahjong.Testing;

using TA = NMahjong.Japanese.TileAnnotations;

namespace NMahjong.Japanese
{
    [TestFixture]
    public class TileAnnotationsExtensionTest : TestHelper
    {
        [Test, TestCaseSource("TestIsValidTrueSource")]
        public void TestIsValidTrue(TileAnnotations annotations)
        {
            Assert.IsTrue(annotations.IsValid());
        }

        [Test, TestCaseSource("TestIsValidFalseSource")]
        public void TestIsValidFalse(TileAnnotations annotations)
        {
            Assert.IsFalse(annotations.IsValid());
        }

        #pragma warning disable 414
        private static readonly TileAnnotations[]
        TestIsValidTrueSource = {
            // None is always valid.
            TA.None,
            // All flags are valid alone.
            TA.Red, TA.Drawn, TA.Claimed, TA.Extending, TA.Riichi,
            // Some maximal combinations.
            TA.Red | TA.Drawn | TA.Extending,
            TA.Red | TA.Drawn | TA.Claimed | TA.Riichi,
        };

        private static readonly TileAnnotations[]
        TestIsValidFalseSource = {
            // Values out of range.
            (TileAnnotations) (-1),
            (TileAnnotations) 32,
            (TileAnnotations) 9999,
            // Invalid flag combinations.
            TA.Claimed | TA.Extending,
            TA.Riichi | TA.Extending,
        };
        #pragma warning restore 414
    }
}
