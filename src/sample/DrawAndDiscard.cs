using System.Linq;
using NMahjong.Base;
using NMahjong.Japanese;

using TA = NMahjong.Japanese.TileAnnotations;

namespace NMahjong.Sample
{
    public class DrawAndDiscard : IIntelligenceFactory
    {
        public Intelligence Create(IntelligenceArgs args,
                                   IEventHandlerRegisterer registerer)
        {
            return new DrawAndDiscardIntelligence(args);
        }
    }

    internal class DrawAndDiscardIntelligence : Intelligence
    {
        internal DrawAndDiscardIntelligence(IntelligenceArgs args)
            : base(args)
        {
        }

        public override IPlayerAction OnTurn()
        {
            return Actions.Discard(Self.Tiles.Single(t => t.Has(TA.Drawn)));
        }

        public override IPlayerAction OnRiichi()
        {
            return RuleAdvisor.GetValidActions().First();  // Should not occur.
        }

        public override IPlayerAction OnDiscard(PlayerId player, AnnotatedTile tile)
        {
            return Actions.None();
        }

        public override IPlayerAction OnKong(PlayerId player, CanonicalTile tile)
        {
            return Actions.None();
        }
    }
}
