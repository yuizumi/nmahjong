using System;
using System.IO;
using NMahjong.Aux;
using NMahjong.Aux.Tools;
using NMahjong.Base;
using NMahjong.Japanese;
using NMahjong.Japanese.Drivers;
using NMahjong.Japanese.Engine;

namespace NMahjong.Drivers.Mjai
{
    internal class MjaiLogConverter : Subcommand<EmptyOptions>
    {
        public override string Description
        {
            get { return "Converts an mjai log into an NMahjong game record."; }
        }

        public override string Name
        {
            get { return "mjaitonmj"; }
        }

        protected override void PrintUsage()
        {
            ErrorOutput.WriteLine("Usage: {0} {1} [INPUT [OUTPUT]]", ProgramName, Name);
        }

        protected override void Run(string[] args, EmptyOptions options)
        {
            using (var disposables = new DisposableContainer()) {
                TextReader reader = (args.Length >= 1)
                    ? disposables.Add(new StreamReader(args[0])) : Console.In;
                TextWriter writer = (args.Length >= 2)
                    ? disposables.Add(new StreamWriter(args[1])) : Console.Out;
                Convert(reader, writer);
            }
        }

        [VisibleForTesting]  // For regression tests.
        internal static void Convert(TextReader reader, TextWriter writer)
        {
            var start = MjaiJson.Parse(reader.ReadLine());
            // TODO(yuizumi): type == 'start_game'.
            var eventSender = new EventSender();
            IGameState gameState = GameState.Create(
                Quad.Of(start.Get<String[]>("names")), MjaiConsts.InitialScores, eventSender);
            new GameRecorder(gameState, writer).RegisterHandlers(eventSender);
            var processor = new MjaiMessageProcessor(eventSender, 0);

            eventSender.OnGameStarted(EventArgs.Empty);
            while (true) {
                var json = MjaiJson.Parse(reader.ReadLine());
                if (json.Type == "end_game") {
                    break;
                }
                processor.Process(json);
            }
            eventSender.OnGameEnded(EventArgs.Empty);
        }
    }
}
