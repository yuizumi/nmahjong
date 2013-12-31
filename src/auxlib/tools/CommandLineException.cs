using System;

namespace NMahjong.Aux.Tools
{
    public class CommandLineException : Exception
    {
        public CommandLineException(string message)
            : base(message)
        {
        }

        public CommandLineException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
