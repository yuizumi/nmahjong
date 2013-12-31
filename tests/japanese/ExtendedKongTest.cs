using NUnit.Framework;
using System;
using NMahjong.Base;
using NMahjong.Testing;

namespace NMahjong.Japanese
{
    [TestFixture]
    public class ExtendedKongTest : TestHelperWithAnnotatedTiles
    {
        [Test]
        public void TestCreate()
        {
            var pung = OpenPung.Create(List(T5r, T5p), T5p, Player1);
            var kong = ExtendedKong.Create(pung, T5p);
            Assert.IsFalse(kong.IsPair);
            Assert.IsFalse(kong.IsChow);
            Assert.IsTrue (kong.IsPung);
            Assert.IsTrue (kong.IsKong);
            Assert.AreEqual(new [] { T5r, T5p, Claimed(T5p), Extending(T5p) },
                            kong.AnnotatedTiles);
            Assert.AreEqual(Kong.Open(T5), kong.BaseMeld);
            Assert.AreEqual(Player1, kong.Feeder);
            Assert.AreEqual(MeldState.Open, kong.State);
            Assert.AreEqual(new [] { T5, T5, T5, T5 }, kong.Tiles);
        }

        [Test]
        public void TestCreateInvalid()
        {
            var pung = OpenPung.Create(List(T5r, T5p), T5p, Player1);
            Assert.Throws<ArgumentException>(() => ExtendedKong.Create(pung, T6p));
        }
    }
}
