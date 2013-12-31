using System;
using System.Collections.Generic;
using System.Linq;
using NMahjong.Base;
using NMahjong.Japanese;

namespace NMahjong.Sample
{
    public class Shanten : IIntelligenceFactory
    {
        public Intelligence Create(IntelligenceArgs args,
                                   IEventHandlerRegisterer registerer)
        {
            return new ShantenIntelligence(args);
        }
    }

    internal class ShantenIntelligence : Intelligence
    {
        private readonly Random mRandom;

        internal ShantenIntelligence(IntelligenceArgs args)
            : base(args)
        {
            mRandom = new Random();
        }

        public override IPlayerAction OnTurn()
        {
            if (IsValidAction(Actions.Mahjong())) {
                return Actions.Mahjong();
            }
            if (IsValidAction(Actions.Riichi())) {
                return Actions.Riichi();
            }

            int[] vector = ShantenHelper.GetVector(Self.Tiles);

            var choices = new List<IPlayerAction>();
            int minShanten = Int32.MaxValue;
            foreach (var action in RuleAdvisor.GetValidActions().OfType<DiscardAction>()) {
                --vector[action.Tile.BaseTile.GetIndex()];
                int shanten = ShantenHelper.ComputeShantensu(vector, (Self.Tiles.Count - 2) / 3);
                if (shanten <= minShanten) {
                    if (shanten < minShanten) choices.Clear();
                    minShanten = shanten;
                    choices.Add(action);
                }
                ++vector[action.Tile.BaseTile.GetIndex()];
            }

            return Choose(choices);
        }

        public override IPlayerAction OnRiichi()
        {
            return Choose(RuleAdvisor.GetValidActions());
        }

        public override IPlayerAction OnDiscard(PlayerId player, AnnotatedTile tile)
        {
            return IsValidAction(Actions.Mahjong()) ? Actions.Mahjong() : Actions.None();
        }

        public override IPlayerAction OnKong(PlayerId player, CanonicalTile tile)
        {
            return IsValidAction(Actions.Mahjong()) ? Actions.Mahjong() : Actions.None();
        }

        protected IPlayerAction Choose(IList<IPlayerAction> choices)
        {
            return choices[mRandom.Next(choices.Count)];
        }

        protected bool IsValidAction(IPlayerAction action)
        {
            return RuleAdvisor.GetValidActions().Any(a => Actions.AreIdentical(a, action));
        }
    }
}
