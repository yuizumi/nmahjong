using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using NMahjong.Aux;
using NMahjong.Aux.Tools;
using NMahjong.Base;
using NMahjong.Japanese;
using NMahjong.Japanese.Drivers;
using NMahjong.Japanese.Engine;

namespace NMahjong.Drivers.Mjai
{
    internal partial class MjaiClient : Subcommand<MjaiClient.Options>
    {
        private const string None = @"{""type"":""none""}";

        public override string Description
        {
            get { return "Connect AI to an mjai server."; }
        }

        public override string Name
        {
            get { return "client"; }
        }

        protected override void Run(string[] args, Options options)
        {
            IIntelligenceFactory factory = options.Get<IntelligenceOptions>().GetFactory();
            var mjaiOptions = options.Get<MjaiOptions>();
            string room = mjaiOptions.Server.Room;
            string name = mjaiOptions.PlayerName ?? factory.GetType().Name;

            try {
                using (var disposables = new DisposableContainer()) {
                    var spectators = new List<MjaiSpectator>();
                    TextWriter recordWriter = MjaiOptionsHelper.GetTextWriter(
                        disposables, mjaiOptions.RecordFile);
                    if (recordWriter != null) {
                        spectators.Add(new MjaiGameRecorder(recordWriter));
                    }
                    ISimpleConnection connection = MjaiOptionsHelper.OpenConnection(
                        disposables, mjaiOptions);
                    Join(connection, name, room);
                    Play(connection, factory, spectators);
                }
            } catch (SocketException e) {
                throw new CommandLineException("Socket error: " + e.Message, e);
            }
        }

        [VisibleForTesting]
        internal static void Join(ISimpleConnection connection, string name, string room)
        {
            MjaiJson hello = MjaiJson.Parse(connection.Receive());
            CheckField(hello, "type", "hello");
            CheckField(hello, "protocol" , "mjsonp");
            CheckField(hello, "protocol_version", 2);
            connection.Send(MjaiJson.Serialize(new { type = "join", name = name, room = room }));
        }

        [VisibleForTesting]
        internal static void Play(ISimpleConnection connection, IIntelligenceFactory factory,
                                  IEnumerable<MjaiSpectator> spectators)
        {
            MjaiJson start = MjaiJson.Parse(connection.Receive());
            CheckField(start, "type", "start_game");
            int baseId = start.Get<Int32>("id");  // TODO(yuizumi): Range check.
            Quad<String> names = Quad.Of(start.Get<String[]>("names"), baseId);

            EventSender eventSender = new EventSender();
            IGameState gameState = GameState.Create(names, MjaiConsts.InitialScores, eventSender);
            MjaiRuleAdvisor advisor = new MjaiRuleAdvisor(gameState, baseId);
            IntelligenceArgs args = new IntelligenceArgs(gameState, advisor);
            Intelligence intelligence = factory.Create(args, eventSender);
            spectators.ForEach(s => s.Register(gameState, eventSender));

            var decider = new MjaiActionDecider(intelligence);
            decider.RegisterHandlers(eventSender);
            var builder = new MjaiMessageBuilder(baseId);
            var validator = new MjaiMessageValidator(baseId);
            var processor = new MjaiMessageProcessor(eventSender, baseId);

            eventSender.OnGameStarted(EventArgs.Empty);
            connection.Send(None);  // Respond to start_game.
            while (true) {
                MjaiJson json = MjaiJson.Parse(connection.Receive());
                validator.Validate(json);
                advisor.SetMessage(json);
                if (json.Type == "end_game") {
                    break;
                }
                processor.Process(json);
                connection.Send(builder.Build(decider.GetNextAction()));
            }
            eventSender.OnGameEnded(EventArgs.Empty);
        }

        // TODO(yuizumi): Consolidate code with MjaiMessageValidator.
        private static void CheckField<T>(MjaiJson json, string field, T expected)
        {
            if (!Object.Equals(json.Get<T>(field), expected)) {
                string message = String.Format("Value must be {0}.", expected);
                throw new MjaiInvalidFieldException(field, json, message);
            }
        }
    }
}
