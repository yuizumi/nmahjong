using NUnit.Framework;
using System;
using NMahjong.Base;
using NMahjong.Testing;

using MS = NMahjong.Base.MeldState;

namespace NMahjong.Japanese
{
    [TestFixture]
    public class OpenChowTest : TestHelperWithAnnotatedTiles
    {
        [Test]
        public void TestCreate()
        {
            var chow = OpenChow.Create(List(T5r, T6p), T7p, Player1);
            Assert.IsFalse(chow.IsPair);
            Assert.IsTrue (chow.IsChow);
            Assert.IsFalse(chow.IsPung);
            Assert.IsFalse(chow.IsKong);
            Assert.AreEqual(new [] { T5r, T6p, Claimed(T7p) }, chow.AnnotatedTiles);
            Assert.AreEqual(Chow.Of(T5, MS.Open), chow.BaseMeld);
            Assert.AreEqual(Player1, chow.Feeder);
            Assert.AreEqual(MS.Open, chow.State);
            Assert.AreEqual(new [] { T5, T6, T7 }, chow.Tiles);
        }

        [Test]
        public void TestCreateReorder()
        {
            var chow = OpenChow.Create(List(T6p, T7p), T5r, Player1);
            Assert.IsFalse(chow.IsPair);
            Assert.IsTrue (chow.IsChow);
            Assert.IsFalse(chow.IsPung);
            Assert.IsFalse(chow.IsKong);
            Assert.AreEqual(new [] { Claimed(T5r), T6p, T7p }, chow.AnnotatedTiles);
            Assert.AreEqual(Chow.Of(T5, MS.Open), chow.BaseMeld);
            Assert.AreEqual(Player1, chow.Feeder);
            Assert.AreEqual(MS.Open, chow.State);
            Assert.AreEqual(new [] { T5, T6, T7 }, chow.Tiles);
        }

        [Test]
        public void TestCreateInvalid()
        {
            Assert.Throws<ArgumentException>(
                () => OpenChow.Create(List(T5r, T5p), T5p, Player1));
        }
    }
}
