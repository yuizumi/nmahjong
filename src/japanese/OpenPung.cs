using System;
using System.Collections.Generic;
using System.Linq;
using NMahjong.Aux;
using NMahjong.Base;

namespace NMahjong.Japanese
{
    public class OpenPung : RevealedMeld
    {
        private OpenPung(IEnumerable<AnnotatedTile> annotatedTiles,
                         PlayerId feeder, Pung baseMeld)
            : base(annotatedTiles, feeder, baseMeld)
        {
        }

        public static OpenPung Create(IEnumerable<CanonicalTile> exposedTiles,
                                      CanonicalTile claimedTile,
                                      PlayerId feeder)
        {
            IEnumerable<AnnotatedTile> tiles = RevealedMeldsHelper
                .ProcessTiles(exposedTiles, claimedTile);
            Pung pung = Pung.Find(tiles.Select(a => a.BaseTile), MeldState.Open);
            CheckArg.Expect(pung != null, "Tiles must form a pung");
            return new OpenPung(tiles, feeder, pung);
        }

        public override string ToString()
        {
            return String.Format("OpenPung({0}, {1})", AnnotatedTiles.BracedString(), Feeder);
        }
    }
}
