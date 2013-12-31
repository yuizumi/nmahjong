using System.Collections.Generic;
using System.Linq;
using NMahjong.Aux;

namespace NMahjong.Base
{
    internal static class MeldsHelper
    {
        internal static TMeld Find<TMeld>(ImmutableDictionary<Tile, TMeld> meldsMap,
                                          IEnumerable<Tile> tiles)
            where TMeld : Meld
        {
            Tile head = tiles.FirstOrDefault();
            TMeld meld;
            if (head == null || !meldsMap.TryGetValue(head, out meld)) {
                return null;
            }
            return meld.Tiles.SequenceEqual(tiles) ? meld : null;
        }
    }
}
