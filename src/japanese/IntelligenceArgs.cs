using NMahjong.Aux;

namespace NMahjong.Japanese
{
    public class IntelligenceArgs
    {
        private readonly IGameState mGameState;
        private readonly IRuleAdvisor mRuleAdvisor;

        public IntelligenceArgs(IGameState gameState, IRuleAdvisor ruleAdvisor)
        {
            CheckArg.NotNull(gameState, "gameState");
            mGameState = gameState;
            CheckArg.NotNull(ruleAdvisor, "ruleAdvisor");
            mRuleAdvisor = ruleAdvisor;
        }

        public IGameState GameState
        {
            get { return mGameState; }
        }

        public IRuleAdvisor RuleAdvisor
        {
            get { return mRuleAdvisor; }
        }
    }
}
