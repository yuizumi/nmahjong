using NMahjong.Aux;

namespace NMahjong.Base
{
    /**
      <summary>
        Represents a set of tiles, such as a chow, a pung, a kong, and a pair.
      </summary>
      <remarks>
        <note>
          Pairs are generally not considered to be melds, but NMahjong regards pairs as
          melds to simplify the logic.
        </note>
      </remarks>
    */
    public abstract class Meld
    {
        /**
          <summary>
            Gets whether this meld forms a pair.
          </summary>
          <value>
            <see langword="true"/> if this meld is a pair or equivalent;
            otherwise, <see langword="false"/>.
          </value>
        */
        public virtual bool IsPair
        {
            get { return false; }
        }

        /**
          <summary>
            Gets whether this meld forms a chow.
          </summary>
          <value>
            <see langword="true"/> if this meld is a chow or equivalent;
            otherwise, <see langword="false"/>.
          </value>
          <remarks>
            <para>
              This property can return <see langword="true"/> for melds that are not standard
              chows. Such melds include knitted chows in Mahjong Competition Rule.
            </para>
          </remarks>
        */
        public virtual bool IsChow
        {
            get { return false; }
        }

        /**
          <summary>
            Gets whether this meld forms a pung.
          </summary>
          <value>
            <see langword="true"/> if this meld is a pung or equivalent;
            otherwise, <see langword="false"/>.
          </value>
          <remarks>
            <para>
              This property can return <see langword="true"/> for melds that are not standard
              pungs. A typical example is kongs.
            </para>
          </remarks>
        */
        public virtual bool IsPung
        {
            get { return false; }
        }

        /**
          <summary>
            Gets whether this meld forms a kong.
          </summary>
          <value>
            <see langword="true"/> if this meld is a kong or equivalent;
            otherwise, <see langword="false"/>.
          </value>
        */
        public virtual bool IsKong
        {
            get { return false; }
        }

        /**
          <summary>
            Gets tiles contained in this meld.
          </summary>
          <value>
            An <see cref="ImmutableList{T}"/> containing the tiles that compose this meld.
          </value>
        */
        public abstract ImmutableList<Tile> Tiles { get; }

        /**
          <summary>
            Gets whether this meld is concealed or open.
          </summary>
          <value>
            <see cref="MeldState"/> representing whether this meld is concealed or open.
          </value>
        */
        public abstract MeldState State { get; }
    }
}
