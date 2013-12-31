using System;

namespace NMahjong.Drivers.Mjai
{
    internal class MjaiJsonException : Exception
    {
        private const string DefaultMessage = "Message is not valid.";

        internal MjaiJsonException() : base(DefaultMessage)
        {
        }

        internal MjaiJsonException(string message)
            : base(message)
        {
        }

        internal MjaiJsonException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
