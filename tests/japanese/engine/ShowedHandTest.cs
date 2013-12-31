using NUnit.Framework;
using System;
using System.Collections.Generic;
using NMahjong.Testing;

namespace NMahjong.Japanese.Engine
{
    [TestFixture]
    public class ShowedHandTest : TestHelperWithAnnotatedTiles
    {
        private static ShowedHand CreateHand(params AnnotatedTile[] tiles)
        {
            return new ShowedHand(new List<AnnotatedTile>(tiles));
        }

        private static ShowedHand CreateHand(IEnumerable<AnnotatedTile> tiles)
        {
            return new ShowedHand(new List<AnnotatedTile>(tiles));
        }

        [Test, TestCaseSource("TestDiscardSource")]
        public void TestDiscard(AnnotatedTile discard, IList<AnnotatedTile> expected)
        {
            ShowedHand hand = CreateHand(T5r, T5p, T6p, Drawn(T7p), FEp);
            hand.Discard(discard);
            Assert.AreEqual(expected, hand);
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestDiscardSource = {
            Data(T5p, List<AnnotatedTile>(T5r, T6p, T7p, FEp)),
            Data(T5r, List<AnnotatedTile>(T5p, T6p, T7p, FEp)),
            Data(Drawn(T7p), List<AnnotatedTile>(T5r, T5p, T6p, FEp)),
            Data(Riichi(T5p), List<AnnotatedTile>(T5r, T6p, T7p, FEp)),
            Data(Riichi(Drawn(T7p)), List<AnnotatedTile>(T5r, T5p, T6p, FEp)),
        };
        #pragma warning restore 414

        [Test, TestCaseSource("TestDiscardNonexistentSource")]
        public void TestDiscardNonexistent(AnnotatedTile discard)
        {
            ShowedHand hand = CreateHand(T5r, T5p, T6p, Drawn(T7p), FEp);
            var ex = Assert.Throws<ArgumentException>(() => hand.Discard(discard));
            Assert.AreEqual("tile", ex.ParamName);
            Assert.AreEqual(new [] { T5r, T5p, T6p, Drawn(T7p), FEp }, hand);
        }

        #pragma warning disable 414
        private static readonly AnnotatedTile[]
        TestDiscardNonexistentSource = {
            FSp, T7p, Drawn(T5p), Riichi(T7p), Riichi(Drawn(T5p)),
        };
        #pragma warning restore 414

        [Test]
        public void TestDraw()
        {
            ShowedHand hand = CreateHand(T5r, T5p, T6p, FEp);
            hand.Draw(T7p);
            Assert.AreEqual(new [] { T5r, T5p, T6p, FEp, Drawn(T7p) }, hand);
        }

        [Test]
        public void TestExcludeWithoutDrawn()
        {
            ShowedHand hand = CreateHand(T5r, T5p, T5p, FEp);
            hand.Exclude(new [] { T5p, T5r });
            Assert.AreEqual(new [] { T5p, FEp }, hand);
        }

        [Test]
        public void TestExcludeWithDrawn()
        {
            ShowedHand hand = CreateHand(T5r, T5p, T5p, FEp, Drawn(T5p));
            hand.Exclude(new [] { T5p, T5p, T5p, T5r });
            Assert.AreEqual(new [] { FEp }, hand);
        }

        [Test, TestCaseSource("TestExcludeInvalidSource")]
        public void TestExcludeInvalid(IList<AnnotatedTile> initialTiles,
                                       IList<CanonicalTile> excludeTiles)
        {
            ShowedHand hand = CreateHand(initialTiles);
            var ex = Assert.Throws<ArgumentException>(() => hand.Exclude(excludeTiles));
            Assert.AreEqual("tiles", ex.ParamName);
            Assert.AreEqual(initialTiles, hand);
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestExcludeInvalidSource = {
            Data(List<AnnotatedTile>(T5r, T5p, T6p, FEp), List(FSp)),
            Data(List<AnnotatedTile>(T5r, T5p, T6p, FEp), List(FEp, FEp)),
            Data(List<AnnotatedTile>(T5r, T5p, T5p, FEp), List(T5p, T5p, T5p)),
        };
        #pragma warning restore 414
    }
}
