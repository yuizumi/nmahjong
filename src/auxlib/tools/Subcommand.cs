namespace NMahjong.Aux.Tools
{
    public abstract class Subcommand<TOptions> : CommandWithOptions<TOptions>, ISubcommand
        where TOptions : OptionSetProvider, new()
    {
        public virtual string Description
        {
            get { return null; }
        }

        public abstract string Name { get; }

        protected override void PrintUsage()
        {
            ErrorOutput.WriteLine("Usage: {0} {1} [OPTIONS]", ProgramName, Name);
            if (Description != null) ErrorOutput.WriteLine(Description);
        }
    }
}
