using NMahjong.Aux;
using NMahjong.Base;

namespace NMahjong.Japanese
{
    public abstract class Intelligence
    {
        private readonly IntelligenceArgs mArgs;

        protected Intelligence(IntelligenceArgs args)
        {
            CheckArg.NotNull(args, "args");
            mArgs = args;
        }

        public IGameState GameState
        {
            get { return mArgs.GameState; }
        }

        public IPlayerState Self
        {
            get { return mArgs.GameState.Players[0]; }
        }

        public IRuleAdvisor RuleAdvisor
        {
            get { return mArgs.RuleAdvisor; }
        }

        public abstract IPlayerAction OnTurn();
        public abstract IPlayerAction OnRiichi();
        public abstract IPlayerAction OnDiscard(PlayerId player, AnnotatedTile tile);
        public abstract IPlayerAction OnKong(PlayerId player, CanonicalTile tile);
    }
}
