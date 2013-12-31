using System;
using System.Collections.Generic;
using System.Linq;
using NMahjong.Aux;
using NMahjong.Base;

namespace NMahjong.Japanese
{
    public class OpenKong : RevealedMeld
    {
        private OpenKong(IEnumerable<AnnotatedTile> annotatedTiles,
                         PlayerId feeder, Kong baseMeld)
            : base(annotatedTiles, feeder, baseMeld)
        {
        }

        public static OpenKong Create(IEnumerable<CanonicalTile> exposedTiles,
                                      CanonicalTile claimedTile,
                                      PlayerId feeder)
        {
            IEnumerable<AnnotatedTile> tiles = RevealedMeldsHelper
                .ProcessTiles(exposedTiles, claimedTile);
            Kong kong = Kong.Find(tiles.Select(a => a.BaseTile), MeldState.Open);
            CheckArg.Expect(kong != null, "Tiles must form a kong");
            return new OpenKong(tiles, feeder, kong);
        }

        public override string ToString()
        {
            return String.Format("OpenKong({0}, {1})", AnnotatedTiles.BracedString(), Feeder);
        }
    }
}
