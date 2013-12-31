using NMahjong.Aux;
using NMahjong.Base;

namespace NMahjong.Japanese
{
    public interface IPlayerState
    {
        string Name { get; }
        int Score { get; }
        Wind SeatWind { get; }
        IPlayerHand Tiles { get; }
        ReadOnlyListView<AnnotatedTile> Discards { get; }
        ReadOnlyListView<RevealedMeld> Melds { get; }
    }
}
