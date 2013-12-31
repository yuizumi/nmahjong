using NUnit.Framework;
using NMahjong.Base;
using NMahjong.Testing;

using TA = NMahjong.Japanese.TileAnnotations;

namespace NMahjong.Japanese
{
    [TestFixture]
    public class CanonicalTileTest : TestHelperWithTiles
    {
        [Test]
        public void TestPlain()
        {
            var ct = CanonicalTile.Plain(T5);
            Assert.AreEqual(T5, ct.BaseTile);
            Assert.AreEqual(TA.None, ct.Annotations);
        }

        [Test]
        public void TestRed()
        {
            var ct = CanonicalTile.Red(T5);
            Assert.AreEqual(T5, ct.BaseTile);
            Assert.AreEqual(TA.Red, ct.Annotations);
        }
    }
}
