using System;
using Mono.Options;
using NMahjong.Aux.Tools;

namespace NMahjong.Drivers.Mjai
{
    internal class MjaiOptions : OptionSetProvider
    {
        internal MjaiUri Server
        {
            get; private set;
        }

        internal string ProtocolFile
        {
            get; private set;
        }

        internal string RecordFile
        {
            get; private set;
        }

        internal string PlayerName
        {
            get; private set;
        }

        public override OptionSet GetOptionSet()
        {
            return new OptionSet() {
                {"<>", "Specify the server.", ParseMjaiUri, true /* hidden */},
                {"p|protocol=", "Output protocols to a file.",
                 arg => { ProtocolFile = arg; }},
                {"r|record=", "Output game records to a file.",
                 arg => { RecordFile = arg; }},
                {"n|name=", "Specify the player name.", arg => { PlayerName = arg; }},
            };
        }

        public override void OnParseComplete()
        {
            if (Server == null) {
                throw new OptionException("No server is specified.", "<>");
            }
        }

        private void ParseMjaiUri(string arg)
        {
            if (Server != null) {
                throw new OptionException("Too many arguments.", null);
            }
            try {
                Server = MjaiUri.Parse(arg);
            } catch (UriFormatException e) {
                throw new OptionException(String.Format("Invalid server '{0}'.", arg), null, e);
            } catch (ArgumentException e) {
                throw new OptionException(String.Format("Invalid server '{0}'.", arg), null, e);
            }
        }
    }
}
