using NMahjong.Aux;

namespace NMahjong.Base
{
    public abstract class Meld
    {
        public virtual bool IsPair
        {
            get { return false; }
        }

        public virtual bool IsChow
        {
            get { return false; }
        }

        public virtual bool IsPung
        {
            get { return false; }
        }

        public virtual bool IsKong
        {
            get { return false; }
        }

        public abstract ImmutableList<Tile> Tiles { get; }
        public abstract MeldState State { get; }
    }
}
