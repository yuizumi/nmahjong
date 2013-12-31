using System;
using System.Linq;

namespace NMahjong.Aux.Tools
{
    public class SubcommandDispatcher : Command
    {
        private readonly SubcommandSet mSubcommands;

        public SubcommandDispatcher(params ISubcommand[] subcommands)
        {
            CheckArg.NotContainsNull(subcommands, "subcommands");
            mSubcommands = new SubcommandSet();
            foreach (ISubcommand subcommand in subcommands) {
                string name = subcommand.Name;
                CheckArg.Expect(!String.IsNullOrEmpty(name),
                                "subcommands", "Subcommand must have a non-empty name.");
                CheckArg.Expect(name != "help",
                                "subcommands", "'help' is reserved and cannot be used.");
                mSubcommands.Add(subcommand);
            }
            mSubcommands.Add(new HelpSubcommand(mSubcommands));
        }

        public override int Run(string[] args)
        {
            string subcommand = (args.Length == 0) ? "help" : args[0];
            if (!mSubcommands.Contains(subcommand)) {
                WriteMessage("Unknown subcommand -- {0}.", args[0]);
                WriteMessage("Type '{0} help' to show usage.", ProgramName);
                return ExitFailure;
            }
            return mSubcommands[subcommand].Run(args.Skip(1).ToArray());
        }
    }
}
