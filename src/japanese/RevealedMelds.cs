using System.Collections.Generic;
using System.Linq;
using NMahjong.Aux;
using NMahjong.Base;

namespace NMahjong.Japanese
{
    public static class RevealedMelds
    {
        public static bool AreIdentical(RevealedMeld a, RevealedMeld b)
        {
            CheckArg.NotNull(a, "a");
            CheckArg.NotNull(b, "b");

            if (a.GetType() != b.GetType() || a.BaseMeld != b.BaseMeld) {
                return false;
            }
            if (a.IsOpen() && a.Feeder != b.Feeder) {
                return false;
            }
            var tally = new List<AnnotatedTile>(a.AnnotatedTiles);
            return b.AnnotatedTiles.All(tally.Remove) && tally.Count == 0;
        }
    }
}
