using System;
using NMahjong.Aux;
using NMahjong.Japanese;

namespace NMahjong.Drivers.Mjai
{
    internal abstract class MjaiSpectator
    {
        protected MjaiSpectator()
        {
        }

        protected IGameState GameState
        {
            get; private set;
        }

        internal void Register(IGameState gameState, IEventHandlerRegisterer registerer)
        {
            GameState = gameState;
            RegisterHandlers(registerer);
        }

        protected abstract void RegisterHandlers(IEventHandlerRegisterer registerer);
    }
}
