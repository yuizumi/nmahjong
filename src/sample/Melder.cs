using System.Collections.Generic;
using System.Linq;
using NMahjong.Aux;
using NMahjong.Base;
using NMahjong.Japanese;

namespace NMahjong.Sample
{
    public class Melder : IIntelligenceFactory
    {
        public Intelligence Create(IntelligenceArgs args,
                                   IEventHandlerRegisterer registerer)
        {
            return new MelderIntelligence(args);
        }
    }

    internal class MelderIntelligence : ShantenIntelligence
    {
        internal MelderIntelligence(IntelligenceArgs args)
            : base(args)
        {
        }

        public override IPlayerAction OnTurn()
        {
            if (IsValidAction(Actions.Mahjong())) {
                return Actions.Mahjong();
            }
            // Riichi, ConcealedKong, ExtendedKong.
            List<IPlayerAction> actions = RuleAdvisor.GetValidActions()
                .Where(a => !(a is DiscardAction)).ToList();
            return (actions.IsEmpty()) ? base.OnTurn() : Choose(actions);
        }

        public override IPlayerAction OnDiscard(PlayerId player, AnnotatedTile tile)
        {
            if (IsValidAction(Actions.Mahjong())) {
                return Actions.Mahjong();
            }
            // Pung, Chow, OpenKong.
            List<IPlayerAction> actions = RuleAdvisor.GetValidActions()
                .Where(a => !(a is NoneAction)).ToList();
            return (actions.IsEmpty()) ? Actions.None() : Choose(actions);
        }
    }
}
