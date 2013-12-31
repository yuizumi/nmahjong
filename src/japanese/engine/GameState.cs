using System;
using NMahjong.Aux;
using NMahjong.Base;

namespace NMahjong.Japanese.Engine
{
    public static class GameState
    {
        public static IGameState Create(Quad<String> names, Quad<Int32> scores,
                                        IEventHandlerRegisterer registerer)
        {
            CheckArg.NotContainsNull(names, "names");
            CheckArg.NotNull(scores, "scores");
            CheckArg.NotNull(registerer, "registerer");

            var gameState = new GameStateImpl(names, scores);
            gameState.RegisterHandlers(registerer);

            return gameState;
        }
    }
}
