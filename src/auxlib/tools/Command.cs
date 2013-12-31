using System;
using System.IO;

namespace NMahjong.Aux.Tools
{
    public abstract class Command
    {
        public const int ExitSuccess = 0;
        public const int ExitFailure = 1;

        protected Command()
        {
            ProgramName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);
        }

        static Command()
        {
            ErrorOutput = Console.Error;
        }

        [VisibleForTesting]
        public string ProgramName
        {
            get; internal set;
        }

        [VisibleForTesting]
        public static TextWriter ErrorOutput
        {
            get; internal set;
        }

        public abstract int Run(string[] args);

        protected void WriteMessage(string message)
        {
            ErrorOutput.WriteLine("{0}: {1}", ProgramName, message);
        }

        protected void WriteMessage(string messageFormat,
                                    params object[] messageArgs)
        {
            ErrorOutput.WriteLine(ProgramName + ": " + messageFormat, messageArgs);
        }
    }
}
