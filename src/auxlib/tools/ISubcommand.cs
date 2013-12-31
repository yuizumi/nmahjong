namespace NMahjong.Aux.Tools
{
    public interface ISubcommand
    {
        string Description { get; }
        string Name { get; }

        int Run(string[] args);
    }
}
