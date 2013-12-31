using System;
using System.Collections.Generic;
using System.Linq;
using NMahjong.Aux;
using NMahjong.Base;

namespace NMahjong.Japanese
{
    public class ConcealedKong : RevealedMeld
    {
        private ConcealedKong(IEnumerable<AnnotatedTile> annotatedTiles,
                              Kong baseMeld)
            : base(annotatedTiles, null, baseMeld)
        {
        }

        public static ConcealedKong Create(IEnumerable<CanonicalTile> exposedTiles)
        {
            IEnumerable<AnnotatedTile> tiles = RevealedMeldsHelper
                .ProcessTiles(exposedTiles);
            Kong kong = Kong.Find(tiles.Select(a => a.BaseTile), MeldState.Concealed);
            CheckArg.Expect(kong != null, "Tiles must form a kong");
            return new ConcealedKong(tiles, kong);
        }

        public override string ToString()
        {
            return String.Format("ConcealedKong({0})", AnnotatedTiles.BracedString());
        }
    }
}
