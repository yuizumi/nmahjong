using System;
using System.Collections.Generic;
using NMahjong.Aux;
using NMahjong.Base;
using NMahjong.Japanese;

namespace NMahjong.Drivers.Mjai
{
    internal static class MjaiConsts
    {
        internal static readonly ImmutableDictionary<String, CanonicalTile>
            MjaiToTile = ImmutableDictionary.Of(BuildMjaiToTile());

        private static Dictionary<String, CanonicalTile> BuildMjaiToTile()
        {
            Func<Tile, CanonicalTile> p = CanonicalTile.Plain;
            Func<Tile, CanonicalTile> r = CanonicalTile.Red;
            return new Dictionary<String, CanonicalTile>() {
                {"?", null},  // Secret tile.
                {"1p", p(Tile.T1)}, {"2p", p(Tile.T2)}, {"3p", p(Tile.T3)},
                {"4p", p(Tile.T4)}, {"5p", p(Tile.T5)}, {"6p", p(Tile.T6)},
                {"7p", p(Tile.T7)}, {"8p", p(Tile.T8)}, {"9p", p(Tile.T9)},
                {"1s", p(Tile.S1)}, {"2s", p(Tile.S2)}, {"3s", p(Tile.S3)},
                {"4s", p(Tile.S4)}, {"5s", p(Tile.S5)}, {"6s", p(Tile.S6)},
                {"7s", p(Tile.S7)}, {"8s", p(Tile.S8)}, {"9s", p(Tile.S9)},
                {"1m", p(Tile.W1)}, {"2m", p(Tile.W2)}, {"3m", p(Tile.W3)},
                {"4m", p(Tile.W4)}, {"5m", p(Tile.W5)}, {"6m", p(Tile.W6)},
                {"7m", p(Tile.W7)}, {"8m", p(Tile.W8)}, {"9m", p(Tile.W9)},
                {"E", p(Tile.FE)}, {"S", p(Tile.FS)}, {"W", p(Tile.FW)}, {"N", p(Tile.FN)},
                {"P", p(Tile.JP)}, {"F", p(Tile.JF)}, {"C", p(Tile.JC)},
                {"5pr", r(Tile.T5)}, {"5sr", r(Tile.S5)}, {"5mr", r(Tile.W5)},
            };
        }

        internal static readonly ImmutableDictionary<String, Wind>
            MjaiToWind = ImmutableDictionary.Of(BuildMjaiToWind());

        private static Dictionary<String, Wind> BuildMjaiToWind()
        {
            return new Dictionary<String, Wind>() {
                {"E", Wind.East}, {"S", Wind.South}, {"W", Wind.West}, {"N", Wind.North},
            };
        }

        internal static readonly ImmutableDictionary<String, DrawHand>
            MjaiToDrawHand = ImmutableDictionary.Of(BuildMjaiToDrawHand());

        private static IDictionary<String, DrawHand> BuildMjaiToDrawHand()
        {
            return new Dictionary<String, DrawHand>() {
                {"fanpai", DrawHand.Exhaustive},
                {"sukaikan", DrawHand.FourKongs},
                {"suchareach", DrawHand.FourRiichi},
                {"sufonrenta", DrawHand.FourWindDiscardsInRow},
                {"kyushukyuhai", DrawHand.NineTerminalsAndHonors},
                {"sanchaho", DrawHand.ThreeWinners},
            };
        }

        internal static readonly Quad<Int32> InitialScores = Quad.Of(_ => 25000);
    }
}
