using System;
using Mono.Options;

namespace NMahjong.Aux.Tools
{
    public class StrictOptionSet : OptionSet
    {
        protected override bool Parse(string argument, OptionContext c)
        {
            if (base.Parse(argument, c)) {
                return true;
            }
            string head, name, sep, value;
            if (!GetOptionParts(argument, out head, out name, out sep, out value)) {
                return false;
            }
            throw new OptionException(
                String.Format("Unknown option '{0}{1}'.", head, name), head + name);
        }

        public static StrictOptionSet CreateFrom(OptionSet source)
        {
            var optionSet = new StrictOptionSet();
            optionSet.AddRange(source);
            return optionSet;
        }
    }
}
