using System;
using System.Collections.Generic;
using System.Linq;
using NMahjong.Aux;
using NMahjong.Base;

using TA = NMahjong.Japanese.TileAnnotations;

namespace NMahjong.Japanese
{
    public class ExtendedKong : RevealedMeld
    {
        private ExtendedKong(IEnumerable<AnnotatedTile> annotatedTiles,
                             PlayerId feeder, Kong baseMeld)
            : base(annotatedTiles, feeder, baseMeld)
        {
        }

        public static ExtendedKong Create(OpenPung basePung,
                                          CanonicalTile extender)
        {
            CheckArg.NotNull(basePung, "basePung");
            CheckArg.NotNull(extender, "extender");
            IEnumerable<AnnotatedTile> tiles = basePung.AnnotatedTiles.Append(
                extender.With(TA.Extending));
            Kong kong = Kong.Find(tiles.Select(a => a.BaseTile), MeldState.Open);
            CheckArg.Expect(kong != null, "The extender must match with the base pung.");
            return new ExtendedKong(tiles, basePung.Feeder, kong);
        }

        public override string ToString()
        {
            return String.Format("ExtendedKong({0}, {1})", AnnotatedTiles.BracedString(), Feeder);
        }
    }
}
