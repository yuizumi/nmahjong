using NUnit.Framework;
using System;
using System.Collections.Generic;
using NMahjong.Base;
using NMahjong.Testing;

namespace NMahjong.Japanese.Engine
{
    [TestFixture]
    public class ImmutableHandTest : TestHelperWithAnnotatedTiles
    {
        [Test]
        public void TestForWaiting()
        {
            var hand = ImmutableHand.ForWaiting(List(T5r, T5p, T6p, T7p));
            Assert.AreEqual(new [] { T5r, T5p, T6p, T7p }, hand);
        }

        [Test]
        public void TestForWinningSelfDraw()
        {
            var hand = ImmutableHand.ForWinning(List(T5r, T5p, T6p, T7p, T5p),
                                                Winning.SelfDraw);
            Assert.AreEqual(new [] { T5r, T5p, T6p, T7p, Drawn(T5p) }, hand);
        }

        [Test]
        public void TestForWinningDiscard()
        {
            var hand = ImmutableHand.ForWinning(List(T5r, T5p, T6p, T7p, T5p),
                                                Winning.Discard);
            Assert.AreEqual(new [] { T5r, T5p, T6p, T7p, Claimed(T5p) }, hand);
        }

        [Test]
        public void TestDiscard()
        {
            var hand = ImmutableHand.ForWaiting(List(T5r, T5p, T6p, T7p));
            Assert.Throws<NotSupportedException>(() => hand.Discard(T5p));
        }

        [Test]
        public void TestDraw()
        {
            var hand = ImmutableHand.ForWaiting(List(T5r, T5p, T6p, T7p));
            Assert.Throws<NotSupportedException>(() => hand.Draw(T5p));
        }

        [Test]
        public void TestExclude()
        {
            var hand = ImmutableHand.ForWaiting(List(T5r, T5p, T6p, T7p));
            Assert.Throws<NotSupportedException>(() => hand.Exclude(List(T5r, T5p)));
        }
    }
}
