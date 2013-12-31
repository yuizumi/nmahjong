using NMahjong.Aux;
using NMahjong.Base;

namespace NMahjong.Japanese
{
    public interface IGameState
    {
        Quad<IPlayerState> Players { get; }
        HandState HandState { get; }
        PlayerId Turn { get; }
        IWall Wall { get; }
        PlayerId Dealer { get; }
        ReadOnlyListView<Dora> Dora { get; }
        Wind PrevailingWind { get; }
        int HandNumber { get; }
        int Counters { get; }
        int RiichiSticks { get; }
    }
}
