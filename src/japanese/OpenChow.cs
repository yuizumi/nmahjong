using System;
using System.Collections.Generic;
using System.Linq;
using NMahjong.Aux;
using NMahjong.Base;

namespace NMahjong.Japanese
{
    public class OpenChow : RevealedMeld
    {
        private OpenChow(IEnumerable<AnnotatedTile> annotatedTiles,
                         PlayerId feeder, Chow baseMeld)
            : base(annotatedTiles, feeder, baseMeld)
        {
        }

        public static OpenChow Create(IEnumerable<CanonicalTile> exposedTiles,
                                      CanonicalTile claimedTile,
                                      PlayerId feeder)
        {
            IEnumerable<AnnotatedTile> tiles = RevealedMeldsHelper
                .ProcessTiles(exposedTiles, claimedTile);
            Chow chow = Chow.Find(tiles.Select(a => a.BaseTile), MeldState.Open);
            CheckArg.Expect(chow != null, "Tiles must form a chow");
            return new OpenChow(tiles.OrderBy(a => a.Rank), feeder, chow);
        }

        public override string ToString()
        {
            return String.Format("OpenChow({0}, {1})", AnnotatedTiles.BracedString(), Feeder);
        }
    }
}
