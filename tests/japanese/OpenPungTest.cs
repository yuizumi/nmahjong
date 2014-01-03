using NUnit.Framework;
using System;
using NMahjong.Base;
using NMahjong.Testing;

using MS = NMahjong.Base.MeldState;

namespace NMahjong.Japanese
{
    [TestFixture]
    public class OpenPungTest : TestHelperWithAnnotatedTiles
    {
        [Test]
        public void TestCreate()
        {
            var pung = OpenPung.Create(List(T5r, T5p), T5p, Player1);
            Assert.IsFalse(pung.IsPair);
            Assert.IsFalse(pung.IsChow);
            Assert.IsTrue (pung.IsPung);
            Assert.IsFalse(pung.IsKong);
            Assert.AreEqual(new [] { T5r, T5p, Claimed(T5p) }, pung.AnnotatedTiles);
            Assert.AreEqual(Pung.Of(T5, MS.Open), pung.BaseMeld);
            Assert.AreEqual(Player1, pung.Feeder);
            Assert.AreEqual(MS.Open, pung.State);
            Assert.AreEqual(new [] { T5, T5, T5 }, pung.Tiles);
        }

        [Test]
        public void TestCreateInvalid()
        {
            Assert.Throws<ArgumentException>(
                () => OpenPung.Create(List(T5r, T6p), T7p, Player1));
        }
    }
}
