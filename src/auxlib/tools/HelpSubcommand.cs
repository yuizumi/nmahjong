using System.Linq;

namespace NMahjong.Aux.Tools
{
    internal class HelpSubcommand : Command, ISubcommand
    {
        private readonly SubcommandSet mSubcommands;

        internal HelpSubcommand(SubcommandSet subcommands)
        {
            mSubcommands = subcommands;
        }

        public string Description
        {
            get { return "Print this message."; }
        }

        public string Name
        {
            get { return "help"; }
        }

        public override int Run(string[] args)
        {
            int maxLength = mSubcommands.Max(cmd => cmd.Name.Length);

            ErrorOutput.WriteLine("Usage: {0} SUBCOMMAND ...", ProgramName);
            ErrorOutput.WriteLine();
            string format = "  {0,-" + maxLength + "}    {1}";
            foreach (ISubcommand subcommand in mSubcommands) {
                ErrorOutput.WriteLine(format, subcommand.Name, subcommand.Description);
            }

            return ExitSuccess;
        }
    }
}
