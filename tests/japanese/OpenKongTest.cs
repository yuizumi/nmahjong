using NUnit.Framework;
using System;
using NMahjong.Base;
using NMahjong.Testing;

using MS = NMahjong.Base.MeldState;

namespace NMahjong.Japanese
{
    [TestFixture]
    public class OpenKongTest : TestHelperWithAnnotatedTiles
    {
        [Test]
        public void TestCreate()
        {
            var kong = OpenKong.Create(List(T5r, T5p, T5p), T5p, Player1);
            Assert.IsFalse(kong.IsPair);
            Assert.IsFalse(kong.IsChow);
            Assert.IsTrue (kong.IsPung);
            Assert.IsTrue (kong.IsKong);
            Assert.AreEqual(new [] { T5r, T5p, T5p, Claimed(T5p) }, kong.AnnotatedTiles);
            Assert.AreEqual(Kong.Of(T5, MS.Open), kong.BaseMeld);
            Assert.AreEqual(Player1, kong.Feeder);
            Assert.AreEqual(MS.Open, kong.State);
            Assert.AreEqual(new [] { T5, T5, T5, T5 }, kong.Tiles);
        }

        [Test]
        public void TestCreateInvalid()
        {
            Assert.Throws<ArgumentException>(
                () => OpenKong.Create(List(T5r, T6p), T7p, Player1));
        }
    }
}
