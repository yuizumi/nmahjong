using NUnit.Framework;
using System;

namespace NMahjong.Japanese.Engine
{
    [TestFixture]
    public class SimpleWallTest
    {
        [Test]
        public void TestCtor()
        {
            var wall = new SimpleWall(70);
            Assert.AreEqual(70, wall.Count);
        }

        [Test]
        public void TestOnTileDrawn()
        {
            var wall = new SimpleWall(70);
            wall.OnTileDrawn(null);
            Assert.AreEqual(69, wall.Count);
        }

        [Test]
        public void TestOnTileDrawnLastTile()
        {
            var wall = new SimpleWall(1);
            wall.OnTileDrawn(null);
            Assert.AreEqual(0, wall.Count);
            Assert.Throws<InvalidOperationException>(() => wall.OnTileDrawn(null));
        }
    }
}
