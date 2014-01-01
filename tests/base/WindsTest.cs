using NUnit.Framework;
using NMahjong.Aux;

namespace NMahjong.Base
{
    [TestFixture]
    public class WindsTest
    {
        [Test]
        public void TestGetTile()
        {
            foreach (Wind wind in Enums.Values<Wind>()) {
                Assert.IsNotNull(wind.GetTile());
            }
        }
    }
}
