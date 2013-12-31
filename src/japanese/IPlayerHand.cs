using System.Collections.Generic;

namespace NMahjong.Japanese
{
    public interface IPlayerHand : IEnumerable<AnnotatedTile>
    {
        AnnotatedTile this[int index] { get; }
        int Count { get; }

        // TODO(yuizumi): Provide APIs to change the tile order.
    }
}
