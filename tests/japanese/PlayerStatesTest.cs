using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using NMahjong.Aux;
using NMahjong.Testing;

namespace NMahjong.Japanese
{
    [TestFixture]
    public class PlayerStatesTest : TestHelperWithAnnotatedTiles
    {
        private IPlayerState mPlayer;

        [SetUp]
        public void Setup()
        {
            mPlayer = Substitute.For<IPlayerState>();
        }

        [Test, TestCaseSource("TestHasRiichiDeclaredSource")]
        public void TestHasRiichiDeclared(IList<AnnotatedTile> discards,
                                          bool expected)
        {
            mPlayer.Discards.Returns(discards.AsReadOnlyView());
            Assert.AreEqual(expected, mPlayer.HasRiichiDeclared());
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestHasRiichiDeclaredSource = {
            Data(List(FEp, Riichi(W1p), Claimed(T5r)), true),
            Data(List(FEp, W1p, Riichi(Claimed(T5r))), true),
            Data(List(FEp, W1p, Claimed(T5r)), false),
            Data(List(FEp, Drawn(W1p), Claimed(T5r)), false),
        };
        #pragma warning restore 414
    }
}
