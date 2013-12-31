using System.Collections.Generic;
using System.Linq;
using NMahjong.Aux;
using NMahjong.Base;

namespace NMahjong.Japanese
{
    public abstract class RevealedMeld : Meld
    {
        private readonly ImmutableList<AnnotatedTile> mAnnotatedTiles;
        private readonly PlayerId? mFeeder;
        private readonly Meld mBaseMeld;

        [FamilyAndAssembly]
        internal RevealedMeld(IEnumerable<AnnotatedTile> annotatedTiles,
                              PlayerId? feeder, Meld baseMeld)
        {
            mAnnotatedTiles = annotatedTiles.ToImmutableList();
            mFeeder = feeder;
            mBaseMeld = baseMeld;
        }

        public ImmutableList<AnnotatedTile> AnnotatedTiles
        {
            get { return mAnnotatedTiles; }
        }

        public Meld BaseMeld
        {
            get { return mBaseMeld; }
        }

        public PlayerId Feeder
        {
            get {
                CheckState.Expect(mFeeder.HasValue, "Meld is concealed.");
                return mFeeder.Value;
            }
        }

        public override bool IsPair { get { return mBaseMeld.IsPair; } }
        public override bool IsChow { get { return mBaseMeld.IsChow; } }
        public override bool IsPung { get { return mBaseMeld.IsPung; } }
        public override bool IsKong { get { return mBaseMeld.IsKong; } }

        public override ImmutableList<Tile> Tiles { get { return mBaseMeld.Tiles; } }
        public override MeldState State { get { return mBaseMeld.State; } }
    }
}
