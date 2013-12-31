using NUnit.Framework;
using System;
using NMahjong.Base;
using NMahjong.Testing;

namespace NMahjong.Japanese
{
    [TestFixture]
    public class ConcealedKongTest : TestHelperWithAnnotatedTiles
    {
        [Test]
        public void TestCreate()
        {
            var kong = ConcealedKong.Create(List(T5r, T5p, T5p, T5p));
            Assert.IsFalse(kong.IsPair);
            Assert.IsFalse(kong.IsChow);
            Assert.IsTrue (kong.IsPung);
            Assert.IsTrue (kong.IsKong);
            Assert.AreEqual(new [] { T5r, T5p, T5p, T5p }, kong.AnnotatedTiles);
            Assert.AreEqual(Kong.Concealed(T5), kong.BaseMeld);
            Assert.Throws<InvalidOperationException>(
                () => { var _ = kong.Feeder; });
            Assert.AreEqual(MeldState.Concealed, kong.State);
            Assert.AreEqual(new [] { T5, T5, T5, T5 }, kong.Tiles);
        }

        [Test]
        public void TestCreateInvalid()
        {
            Assert.Throws<ArgumentException>(
                () => ConcealedKong.Create(List(T5r, T5p, T5p)));
        }
    }
}
