using System.Text;
using NMahjong.Aux;
using NMahjong.Base;

using TA = NMahjong.Japanese.TileAnnotations;

namespace NMahjong.Japanese
{
    public class AnnotatedTile
    {
        private readonly Tile mBaseTile;
        private readonly TileAnnotations mAnnotations;

        internal AnnotatedTile(Tile baseTile, TileAnnotations annotations)
        {
            CheckArg.NotNull(baseTile, "baseTile");
            mBaseTile = baseTile;
            CheckArg.Expect(annotations.IsValid(), "annotations",
                            "Argument contains invalid set of annotations.");
            mAnnotations = annotations;
        }

        public TileAnnotations Annotations
        {
            get { return mAnnotations; }
        }

        public Tile BaseTile
        {
            get { return mBaseTile; }
        }

        public Suit Suit
        {
            get { return mBaseTile.Suit; }
        }

        public int Rank
        {
            get { return mBaseTile.Rank; }
        }

        public override bool Equals(object obj)
        {
            AnnotatedTile a = obj as AnnotatedTile;
            if (a == null) {
                return false;
            } else {
                return mBaseTile == a.mBaseTile && mAnnotations == a.mAnnotations;
            }
        }

        public override int GetHashCode()
        {
            return mBaseTile.GetHashCode() ^ mAnnotations.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder().Append(mBaseTile)
                .Append((mAnnotations & TA.Red) == 0 ? 'p' : 'r');
            TileAnnotations annotations = mAnnotations & ~TA.Red;
            if (annotations != 0) sb.AppendFormat("<{0}>", annotations);
            return sb.ToString();  // TODO(yuizumi): Cache the string?
        }

        public static AnnotatedTile Of(Tile baseTile, TileAnnotations annotations)
        {
            switch (annotations) {
                case TA.None:
                    return CanonicalTile.Plain(baseTile);
                case TA.Red:
                    return CanonicalTile.Red(baseTile);
                default:
                    return new AnnotatedTile(baseTile, annotations);
            }
        }
    }
}
