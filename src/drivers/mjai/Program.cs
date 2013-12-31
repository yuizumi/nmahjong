using NMahjong.Aux.Tools;

namespace NMahjong.Drivers.Mjai
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            var dispatcher = new SubcommandDispatcher(
                new MjaiClient(),
                new MjaiLogConverter());
            return dispatcher.Run(args);
        }
    }
}
