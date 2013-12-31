using NUnit.Framework;
using System;
using NMahjong.Testing;

namespace NMahjong.Japanese
{
    [TestFixture]
    public class WinningInfoTest : TestHelperWithTiles
    {
        [Test]
        public void TestFull()
        {
            WinningInfo mahjongInfo = new WinningInfo.Builder()
                .SetWinner(Player1).SetFeeder(Player2)
                .SetMinipoints(30).SetFan(3).SetPoints(3900).Build();
            Assert.AreEqual(Player1, mahjongInfo.Winner);
            Assert.AreEqual(Player2, mahjongInfo.Feeder);
            Assert.AreEqual(3900, mahjongInfo.Points);
            Assert.AreEqual(3, mahjongInfo.Fan);
            Assert.AreEqual(30, mahjongInfo.Minipoints);
        }

        [Test]
        public void TestMinimal()
        {
            WinningInfo mahjongInfo = new WinningInfo.Builder()
                .SetWinner(Player1).Build();
            Assert.AreEqual(Player1, mahjongInfo.Winner);
            Assert.AreEqual(Player1, mahjongInfo.Feeder);
            Assert.AreEqual(null, mahjongInfo.Points);
            Assert.AreEqual(null, mahjongInfo.Fan);
            Assert.AreEqual(null, mahjongInfo.Minipoints);
        }

        [Test]
        public void TestRequireWinner()
        {
            var builder = new WinningInfo.Builder();
            builder.SetFeeder(Player2);
            Assert.Throws<InvalidOperationException>(() => builder.Build());
            builder.SetWinner(Player1);
            Assert.DoesNotThrow(() => builder.Build());
        }
    }
}
