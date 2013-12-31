using System;
using System.IO;
using NMahjong.Aux;
using NMahjong.Aux.Tools;
using NMahjong.Japanese.Drivers;

namespace NMahjong.Drivers.Mjai
{
    internal static class MjaiOptionsHelper
    {
        internal static TextWriter GetTextWriter(DisposableContainer disposables,
                                                 string filename)
        {
            if (filename == null) {
                return null;
            }
            try {
                switch (filename) {
                    case "/dev/stdout":
                        return Console.Out;
                    case "/dev/stderr":
                        return Console.Error;
                    default:
                        return disposables.Add(new StreamWriter(filename));
                }
            } catch (IOException e) {
                throw new CommandLineException(
                    String.Format("Failed to open {0}: {1}.", filename, e.Message));
            } catch (NotSupportedException e) {
                throw new CommandLineException(
                    String.Format("Failed to open {0}: {1}.", filename, e.Message));
            }
        }

        internal static ISimpleConnection OpenConnection(DisposableContainer disposables,
                                                         MjaiOptions options)
        {
            TextWriter writer = GetTextWriter(disposables, options.ProtocolFile);
            string host = options.Server.Host;
            int port = options.Server.Port;

            if (writer == null) {
                return disposables.Add(new SimpleClient(host, port));
            } else {
                writer.WriteLine("Connecting to {0}:{1}.", host, port);
                return disposables.Add(new LoggedConnection(new SimpleClient(host, port), writer));
            }
        }
    }
}
