using Mono.Options;
using NMahjong.Aux.Tools;

namespace NMahjong.Japanese.Drivers
{
    public class IntelligenceOptions : OptionSetProvider
    {
        public string AssemblyFile
        {
            get; private set;
        }

        public string TypeName
        {
            get; private set;
        }

        public override OptionSet GetOptionSet()
        {
            return new OptionSet() {
                {"a|assembly=", "Specify an assembly to load.", arg => { AssemblyFile = arg; }},
                {"t|type=", "Specify AI implementation.", arg => { TypeName = arg; }},
            };
        }

        public override void OnParseComplete()
        {
            if (AssemblyFile == null) {
                throw new OptionException("No assembly file is specified.", "--assembly");
            }
        }
    }
}
