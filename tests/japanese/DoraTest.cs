using NUnit.Framework;
using NMahjong.Base;
using NMahjong.Testing;

namespace NMahjong.Japanese
{
    [TestFixture]
    public class DoraTest : TestHelperWithAnnotatedTiles
    {
        [Test]
        public void TestSame()
        {
            Dora dora = Dora.Same(T5r);
            Assert.AreEqual(T5r, dora.Indicator);
            Assert.AreEqual(T5, dora.Tile);
        }

        [Test]
        public void TestNext()
        {
            Dora dora = Dora.Next(T5r);
            Assert.AreEqual(T5r, dora.Indicator);
            Assert.AreEqual(T6, dora.Tile);
        }
    }
}
