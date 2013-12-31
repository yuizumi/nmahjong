using NUnit.Framework;
using System;
using System.Collections.Generic;
using NMahjong.Base;
using NMahjong.Japanese;

namespace NMahjong.Drivers.Mjai
{
    [TestFixture]
    public class MjaiConstsTest
    {
        [Test]
        public void TestMjaiToTile()
        {
            var map = new Dictionary<String, CanonicalTile>();
            map.Add("?", null);
            foreach (Tile tile in Tile.AllTiles) {
                switch (tile.Suit) {
                    case Suit.Dots:
                        map.Add(tile.Rank + "p", CanonicalTile.Plain(tile));
                        break;
                    case Suit.Bams:
                        map.Add(tile.Rank + "s", CanonicalTile.Plain(tile));
                        break;
                    case Suit.Craks:
                        map.Add(tile.Rank + "m", CanonicalTile.Plain(tile));
                        break;
                    default:  // F[ESWN]|J[PFC].
                        map.Add(tile.Name.Substring(1), CanonicalTile.Plain(tile));
                        break;
                }
            }
            map.Add("5pr", CanonicalTile.Red(Tile.T5));
            map.Add("5sr", CanonicalTile.Red(Tile.S5));
            map.Add("5mr", CanonicalTile.Red(Tile.W5));
            CollectionAssert.AreEquivalent(map, MjaiConsts.MjaiToTile);
        }

        [Test]
        public void TestMjaiToWind()
        {
            CollectionAssert.AreEquivalent(
                Enum.GetValues(typeof(Wind)), MjaiConsts.MjaiToWind.Values);
        }
    }
}

