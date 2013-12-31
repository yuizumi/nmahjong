using NUnit.Framework;
using System;
using NMahjong.Testing;

using TA = NMahjong.Japanese.TileAnnotations;

namespace NMahjong.Japanese
{
    [TestFixture]
    public class CheckTileTest : TestHelperWithAnnotatedTiles
    {
        [Test, TestCaseSource("TestHasOnlyNoThrowSource")]
        public void TestHasOnlyNoThrow(AnnotatedTile arg,
                                       TileAnnotations annotations)
        {
            Assert.DoesNotThrow(
                () => CheckTile.HasOnly(arg, "arg", annotations));
        }

        [Test, TestCaseSource("TestHasOnlyThrowSource")]
        public void TestHasOnlyThrow(AnnotatedTile arg,
                                     TileAnnotations annotations)
        {
            var ex = Assert.Throws<ArgumentException>(
                () => CheckTile.HasOnly(arg, "arg", annotations));
            Assert.AreEqual("arg", ex.ParamName);
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestHasOnlyNoThrowSource = {
            Data(T5p, TA.Red | TA.Drawn),
            Data(T5r, TA.Red | TA.Drawn),
            Data(Drawn(T5r), TA.Red | TA.Drawn),
        };

        private static readonly TestCaseData[]
        TestHasOnlyThrowSource = {
            Data(Claimed(T5p), TA.Red | TA.Drawn),
            Data(Drawn(T5r), TA.Red),
            Data(Claimed(T5r), TA.Red | TA.Drawn),
        };
        #pragma warning restore 414
    }
}
