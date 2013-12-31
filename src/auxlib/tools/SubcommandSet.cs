using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace NMahjong.Aux.Tools
{
    internal class SubcommandSet : KeyedCollection<String, ISubcommand>
    {
        protected override string GetKeyForItem(ISubcommand subcommand)
        {
            return subcommand.Name;
        }
    }
}
