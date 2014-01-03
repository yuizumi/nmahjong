using NUnit.Framework;
using System.Collections.Generic;
using NMahjong.Testing;

using MS = NMahjong.Base.MeldState;

namespace NMahjong.Base
{
    [TestFixture]
    public class PungTest : TestHelperWithTiles
    {
        [Test, TestCaseSource("TestOfSource")]
        public void TestConcealed(Tile tile, IList<Tile> tiles)
        {
            Pung pung = Pung.Of(tile, MS.Concealed);
            Assert.IsFalse(pung.IsPair);
            Assert.IsFalse(pung.IsChow);
            Assert.IsTrue (pung.IsPung);
            Assert.IsFalse(pung.IsKong);
            Assert.AreEqual(MS.Concealed, pung.State);
            Assert.AreEqual(tiles, pung.Tiles);
        }

        [Test, TestCaseSource("TestOfSource")]
        public void TestOpen(Tile tile, IList<Tile> tiles)
        {
            Pung pung = Pung.Of(tile, MS.Open);
            Assert.IsFalse(pung.IsPair);
            Assert.IsFalse(pung.IsChow);
            Assert.IsTrue (pung.IsPung);
            Assert.IsFalse(pung.IsKong);
            Assert.AreEqual(MS.Open, pung.State);
            Assert.AreEqual(tiles, pung.Tiles);
        }

        [Test, TestCaseSource("TestFindSource")]
        public void TestFind(IList<Tile> tiles, Tile tile)
        {
            Assert.AreEqual(Pung.Of(tile, MS.Concealed), Pung.Find(tiles, MS.Concealed));
            Assert.AreEqual(Pung.Of(tile, MS.Open), Pung.Find(tiles, MS.Open));
        }

        [Test, TestCaseSource("TestFindInvalidSource")]
        public void TestFindInvalid(IList<Tile> tiles)
        {
            Assert.IsNull(Pung.Find(tiles, MS.Concealed));
            Assert.IsNull(Pung.Find(tiles, MS.Open));
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestOfSource = {
            Data(T1, List(T1,T1,T1)), Data(S1, List(S1,S1,S1)), Data(W1, List(W1,W1,W1)),
            Data(T2, List(T2,T2,T2)), Data(S2, List(S2,S2,S2)), Data(W2, List(W2,W2,W2)),
            Data(T3, List(T3,T3,T3)), Data(S3, List(S3,S3,S3)), Data(W3, List(W3,W3,W3)),
            Data(T4, List(T4,T4,T4)), Data(S4, List(S4,S4,S4)), Data(W4, List(W4,W4,W4)),
            Data(T5, List(T5,T5,T5)), Data(S5, List(S5,S5,S5)), Data(W5, List(W5,W5,W5)),
            Data(T6, List(T6,T6,T6)), Data(S6, List(S6,S6,S6)), Data(W6, List(W6,W6,W6)),
            Data(T7, List(T7,T7,T7)), Data(S7, List(S7,S7,S7)), Data(W7, List(W7,W7,W7)),
            Data(T8, List(T8,T8,T8)), Data(S8, List(S8,S8,S8)), Data(W8, List(W8,W8,W8)),
            Data(T9, List(T9,T9,T9)), Data(S9, List(S9,S9,S9)), Data(W9, List(W9,W9,W9)),
            Data(FE, List(FE,FE,FE)), Data(FS, List(FS,FS,FS)),
            Data(FW, List(FW,FW,FW)), Data(FN, List(FN,FN,FN)),
            Data(JP, List(JP,JP,JP)), Data(JF, List(JF,JF,JF)), Data(JC, List(JC,JC,JC)),
        };

        private static readonly TestCaseData[]
        TestFindSource = {
            Data(List(T5,T5,T5), T5), Data(List(FE,FE,FE), FE),
        };

        private static readonly IList<Tile>[]
        TestFindInvalidSource = {
            List(FE,FE,FS), List(FE,FS,FE), List(FS,FE,FE),
            List(T5,T6,T7), List(T5,S5,W5), List(FE,FE), List(FE,FE,FE,FE),
        };
        #pragma warning restore 414
    }
}
