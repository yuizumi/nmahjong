using System;

namespace NMahjong.Japanese
{
    public class InvalidActionException : Exception
    {
        private const string DefaultMessage = "Intelligence reterned an invalid action.";

        public InvalidActionException()
            : base(DefaultMessage)
        {
        }

        public InvalidActionException(string message)
            : base(message)
        {
        }

        public InvalidActionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
