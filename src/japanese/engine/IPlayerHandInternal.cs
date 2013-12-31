using System.Collections.Generic;

namespace NMahjong.Japanese.Engine
{
    internal interface IPlayerHandInternal : IPlayerHand
    {
        void Discard(AnnotatedTile tile);
        void Draw(CanonicalTile tile);
        void Exclude(IEnumerable<CanonicalTile> tiles);
    }
}
