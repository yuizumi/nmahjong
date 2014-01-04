using System;
using NMahjong.Aux;

namespace NMahjong.Base
{
    /**
      <summary>
        Wraps an <see cref="Int32"/> used to identify a player in four-player mahjong.
      </summary>
      <remarks>
        <para>
          <see cref="PlayerId"/> is essentially a wrapper around an <see cref="Int32"/> used
          to identify a player, for which NMahjong uses numbers from 0 to 3. By convention 0
          denotes the player of interest; in AI routines 0 refers to the player of which the
          routine decides the actions. 1, 2, and 3 denote the players on the right, in front,
          and on the left, respectively.
        </para>
        <para>
          Having a dedicated type has two advantages over just using an integer:
        </para>
        <list type="bullet">
          <item>
            The constructor can verify the number to be within the range between 0 and 3.
          </item>
          <item>
            The code can easily indicate values expected to be a player identifier.
          </item>
        </list>
        <note>
          <see cref="PlayerId"/> is not designed for use in three-player mahjong.
        </note>
      </remarks>
    */
    public struct PlayerId : IEquatable<PlayerId>
    {
        /// <summary>Represents the player of interest.</summary>
        public static readonly PlayerId Self = new PlayerId(0);

        private readonly int mId;

        /**
          <summary>
            Initializes a new instance of <see cref="PlayerId"/> with the specified number.
          </summary>
          <param name="id">
            The number to wrap with this object.
          </param>
          <exception cref="ArgumentOutOfRangeException">
            <paramref name="id"/> is not in the range between 0 and 3.
          </exception>
        */
        public PlayerId(int id)
        {
            CheckArg.Range(id, "id", 0, 3);
            mId = id;
        }

        /**
          <summary>
            Gets the number wrapped by the current object.
          </summary>
          <value>
            An <see cref="Int32"/> that identifies a player.
          </value>
        */
        public int Id
        {
            get { return mId; }
        }

        /**
          <summary>
            Compares two instances of <see cref="PlayerId"/> to determine whether
            they are equal.
          </summary>
          <param name="x">
            The first <see cref="PlayerId"/> to compare.
          </param>
          <param name="y">
            The second <see cref="PlayerId"/> to compare.
          </param>
          <returns>
            <see langword="true"/> if <paramref name="x"/> and <paramref name="y"/> have
            the same number; otherwise, <see langword="false"/>.
          </returns>
        */
        public static bool operator ==(PlayerId x, PlayerId y)
        {
            return x.mId == y.mId;
        }

        /**
          <summary>
            Compares two instances of <see cref="PlayerId"/> to determine whether
            they are not equal.
          </summary>
          <param name="x">
            The first <see cref="PlayerId"/> to compare.
          </param>
          <param name="y">
            The second <see cref="PlayerId"/> to compare.
          </param>
          <returns>
            <see langword="true"/> if <paramref name="x"/> and <paramref name="y"/> do not
            have the same number; otherwise, <see langword="false"/>.
          </returns>
        */
        public static bool operator !=(PlayerId x, PlayerId y)
        {
            return x.mId != y.mId;
        }

        /**
          <summary>
            Compares the current object with the specified object for equality.
          </summary>
          <param name="obj">
            The <see cref="Object"/> to compare the current object with.
          </param>
          <returns>
            <see langword="true"/> if <paramref name="obj"/> is <see cref="PlayerId"/> that has
            the same number as the current object; otherwise, <see langword="false"/>.
          </returns>
        */
        public override bool Equals(Object obj)
        {
            return (obj is PlayerId) && mId == ((PlayerId) obj).mId;
        }

        /**
          <summary>
            Compares the current object with the specified object for equality.
          </summary>
          <param name="other">
            The <see cref="PlayerId"/> to compare the current object with.
          </param>
          <returns>
            <see langword="true"/> if <paramref name="other"/> has the same number as the current
            object; otherwise, <see langword="false"/>.
          </returns>
        */
        public bool Equals(PlayerId other)
        {
            return mId == other.mId;
        }

        /**
          <summary>
            Generates a hash code for the current object.
          </summary>
          <returns>
            A hash code for the current object.
          </returns>
        */
        public override int GetHashCode()
        {
            return mId.GetHashCode();
        }

        /**
          <summary>
            Returns a string that represents the current object.
          </summary>
          <returns>
            A string of the form "<c>PlayerId(<i>n</i>)</c>," where <i>n</i> is the number
            wrapped by the current object.
          </returns>
        */
        public override string ToString()
        {
            return String.Format("PlayerId({0})", mId);
        }
    }
}
