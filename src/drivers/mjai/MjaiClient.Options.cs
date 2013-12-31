using Mono.Options;
using NMahjong.Aux.Tools;
using NMahjong.Japanese.Drivers;

namespace NMahjong.Drivers.Mjai
{
    internal partial class MjaiClient
    {
        internal class Options : ComplexOptionSetProvider
        {
            public Options()
            {
                Add(new MjaiOptions());
                Add(new IntelligenceOptions());
            }

            public override OptionSet GetOptionSet()
            {
                return StrictOptionSet.CreateFrom(base.GetOptionSet());
            }
        }
    }
}
