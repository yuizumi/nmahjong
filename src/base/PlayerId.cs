using System;
using NMahjong.Aux;

namespace NMahjong.Base
{
    public struct PlayerId
    {
        public static readonly PlayerId Self = new PlayerId(0);

        private readonly int mId;

        public PlayerId(int id)
        {
            CheckArg.Range(id, "id", 0, 3);
            mId = id;
        }

        public int Id
        {
            get { return mId; }
        }

        public static bool operator ==(PlayerId x, PlayerId y)
        {
            return x.mId == y.mId;
        }

        public static bool operator !=(PlayerId x, PlayerId y)
        {
            return x.mId != y.mId;
        }

        public override bool Equals(Object obj)
        {
            return (obj is PlayerId) && mId == ((PlayerId) obj).mId;
        }

        public override int GetHashCode()
        {
            return mId.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("PlayerId({0})", mId);
        }
    }
}
