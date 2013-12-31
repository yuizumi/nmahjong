using NUnit.Framework;
using System;
using System.Linq;
using NMahjong.Testing;

namespace NMahjong.Japanese.Engine
{
    [TestFixture]
    public class HiddenHandTest : TestHelperWithAnnotatedTiles
    {
        [Test]
        public void TestDiscardNoThrow()
        {
            HiddenHand hand = new HiddenHand(5);
            hand.Discard(Drawn(T7p));
            Assert.AreEqual(Enumerable.Repeat<AnnotatedTile>(null, 4), hand);
        }

        [Test]
        public void TestDiscardThrow()
        {
            HiddenHand hand = new HiddenHand(0);
            Assert.Throws<InvalidOperationException>(() => hand.Discard(Drawn(T7p)));
        }

        [Test]
        public void TestDrawNoThrow()
        {
            HiddenHand hand = new HiddenHand(4);
            hand.Draw(null);
            Assert.AreEqual(Enumerable.Repeat<AnnotatedTile>(null, 5), hand);
        }

        [Test]
        public void TestDrawThrow()
        {
            HiddenHand hand = new HiddenHand(4);
            Assert.Throws<ArgumentException>(() => hand.Draw(T5p));
        }

        [Test]
        public void TestExcludeNoThrow()
        {
            HiddenHand hand = new HiddenHand(4);
            hand.Exclude(List(T5p, T5p, T5r));
            Assert.AreEqual(Enumerable.Repeat<AnnotatedTile>(null, 1), hand);
        }

        [Test]
        public void TestExcludeThrow()
        {
            HiddenHand hand = new HiddenHand(2);
            Assert.Throws<InvalidOperationException>(() => hand.Exclude(List(T5p, T5p, T5r)));
        }
    }
}
