using NUnit.Framework;
using System;
using System.Collections.Generic;
using NMahjong.Testing;

using MS = NMahjong.Base.MeldState;

namespace NMahjong.Base
{
    [TestFixture]
    public class ChowTest : TestHelperWithTiles
    {
        [Test, TestCaseSource("TestOfSource")]
        public void TestConcealed(Tile tile, IList<Tile> tiles)
        {
            Chow chow = Chow.Of(tile, MS.Concealed);
            Assert.IsFalse(chow.IsPair);
            Assert.IsTrue (chow.IsChow);
            Assert.IsFalse(chow.IsPung);
            Assert.IsFalse(chow.IsKong);
            Assert.AreEqual(MS.Concealed, chow.State);
            Assert.AreEqual(tiles, chow.Tiles);
        }

        [Test, TestCaseSource("TestOfSource")]
        public void TestOpen(Tile tile, IList<Tile> tiles)
        {
            Chow chow = Chow.Of(tile, MS.Open);
            Assert.IsFalse(chow.IsPair);
            Assert.IsTrue (chow.IsChow);
            Assert.IsFalse(chow.IsPung);
            Assert.IsFalse(chow.IsKong);
            Assert.AreEqual(MS.Open, chow.State);
            Assert.AreEqual(tiles, chow.Tiles);
        }

        [Test, TestCaseSource("TestOfInvalidSource")]
        public void TestOfInvalid(Tile tile)
        {
            Assert.Throws<ArgumentException>(() => Chow.Of(tile, MS.Concealed));
            Assert.Throws<ArgumentException>(() => Chow.Of(tile, MS.Open));
        }

        [Test, TestCaseSource("TestFindSource")]
        public void TestFind(IList<Tile> tiles, Tile tile)
        {
            Assert.AreEqual(Chow.Of(tile, MS.Concealed), Chow.Find(tiles, MS.Concealed));
            Assert.AreEqual(Chow.Of(tile, MS.Open), Chow.Find(tiles, MS.Open));
        }

        [Test, TestCaseSource("TestFindInvalidSource")]
        public void TestFindInvalid(IList<Tile> tiles)
        {
            Assert.IsNull(Chow.Find(tiles, MS.Concealed));
            Assert.IsNull(Chow.Find(tiles, MS.Open));
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestOfSource = {
            Data(T1, List(T1,T2,T3)), Data(S1, List(S1,S2,S3)), Data(W1, List(W1,W2,W3)),
            Data(T2, List(T2,T3,T4)), Data(S2, List(S2,S3,S4)), Data(W2, List(W2,W3,W4)),
            Data(T3, List(T3,T4,T5)), Data(S3, List(S3,S4,S5)), Data(W3, List(W3,W4,W5)),
            Data(T4, List(T4,T5,T6)), Data(S4, List(S4,S5,S6)), Data(W4, List(W4,W5,W6)),
            Data(T5, List(T5,T6,T7)), Data(S5, List(S5,S6,S7)), Data(W5, List(W5,W6,W7)),
            Data(T6, List(T6,T7,T8)), Data(S6, List(S6,S7,S8)), Data(W6, List(W6,W7,W8)),
            Data(T7, List(T7,T8,T9)), Data(S7, List(S7,S8,S9)), Data(W7, List(W7,W8,W9)),
        };

        private static readonly Tile[]
        TestOfInvalidSource = {
            T8, T9, S8, S9, W8, W9, FE, FS, FW, FN, JP, JF, JC,
        };

        private static readonly TestCaseData[]
        TestFindSource = {
            Data(List(T5,T6,T7), T5), Data(List(T7,T6,T5), T5),
            Data(List(T1,T2,T3), T1), Data(List(T7,T8,T9), T7),
        };

        private static readonly IList<Tile>[]
        TestFindInvalidSource = {
            List(T5,T5,T5), List(T5,S6,W7), List(T8,T9,T1), List(T9,T1,T2),
            List(FE,FS,FW), List(JP,JF,JC), List(T5,T6), List(T5,T6,T7,T8),
        };
        #pragma warning restore 414
    }
}
