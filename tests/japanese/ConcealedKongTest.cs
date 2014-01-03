using NUnit.Framework;
using System;
using NMahjong.Base;
using NMahjong.Testing;

using MS = NMahjong.Base.MeldState;

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
            Assert.AreEqual(Kong.Of(T5, MS.Concealed), kong.BaseMeld);
            Assert.Throws<InvalidOperationException>(() => { var _ = kong.Feeder; });
            Assert.AreEqual(MS.Concealed, kong.State);
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
