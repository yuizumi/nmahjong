using System;
using System.Collections.Generic;
using Mono.Options;

namespace NMahjong.Aux.Tools
{
    public abstract class CommandWithOptions<TOptions> : Command, IHelpMessage
        where TOptions : OptionSetProvider, new()
    {
        public override int Run(string[] args)
        {
            TOptions options = new TOptions();

            OptionSet parser = options.GetOptionSet();
            bool showHelp = false;
            parser.Add("h|?|help", "Print this message.",
                       arg => { showHelp = (arg != null); });

            List<String> otherArgs;
            try {
                otherArgs = parser.Parse(args);
                if (!showHelp) options.OnParseComplete();
            } catch (OptionException e) {
                WriteMessage(e.Message);
                return ExitFailure;
            }

            if (showHelp) {
                PrintUsage();
                ErrorOutput.WriteLine();
                parser.WriteOptionDescriptions(ErrorOutput);
                return ExitSuccess;
            }

            try {
                Run(otherArgs.ToArray(), options);
            } catch (CommandLineException e) {
                WriteMessage(e.Message);
                return ExitFailure;
            }

            return ExitSuccess;
        }

        public void PrintHelpMessage()
        {
            Run(new [] { "--help" });
        }

        protected virtual void PrintUsage()
        {
            ErrorOutput.WriteLine("Usage: {0} [OPTIONS]", ProgramName);
        }

        protected abstract void Run(string[] args, TOptions options);
    }
}
