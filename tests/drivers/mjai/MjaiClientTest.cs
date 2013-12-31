using NUnit.Framework;
using NSubstitute;
using NMahjong.Japanese;
using NMahjong.Japanese.Drivers;

namespace NMahjong.Drivers.Mjai
{
    [TestFixture]
    public class MjaiClientTest
    {
        private class TestSpectator : MjaiSpectator
        {
            public int NumOnGameStarted
            {
                get; private set;
            }

            public int NumOnGameEnded
            {
                get; private set;
            }

            public int NumRegisterHandlers
            {
                get; private set;
            }

            public IGameState GetGameState()
            {
                return GameState;
            }

            protected override void RegisterHandlers(IEventHandlerRegisterer registerer)
            {
                ++NumRegisterHandlers;
                registerer.AddOnGameStarted((sender, e) => { ++NumOnGameStarted; });
                registerer.AddOnGameEnded((sender, e) => { ++NumOnGameEnded; });
            }
        }

        [Test]
        public void TestJoin()
        {
            const string hello = @"{""type"":""hello"",""protocol"":""mjsonp"","
                + @"""protocol_version"":2}";
            var conn = Substitute.For<ISimpleConnection>();
            conn.Receive().Returns(hello);
            MjaiClient.Join(conn, "nmj", "default");
            conn.Received(1).Send(@"{""type"":""join"",""name"":""nmj"",""room"":""default""}");
            conn.ReceivedWithAnyArgs(1).Send(null);
        }

        [Test, TestCaseSource("TestEstablishErrorSource")]
        public void TestJoinError(string message)
        {
            var conn = Substitute.For<ISimpleConnection>();
            conn.Receive().Returns(message);
            Assert.Catch<MjaiJsonException>(() => MjaiClient.Join(conn, "nmj", "default"));
        }

        #pragma warning disable 414
        private static readonly string[]
        TestEstablishErrorSource = {
            @"{""type"":""olleh"",""protocol"":""mjsonp"",""protocol_version"":2}",
            @"{""type"":""hello"",""protocol"":""telnet"",""protocol_version"":2}",
            @"{""type"":""hello"",""protocol"":""mjsonp"",""protocol_version"":1}",
            @"{""type"":""hello"",""protocol"":""mjsonp"",""protocol_version"":3}",
        };
        #pragma warning restore 414

        [Test]
        public void TestPlayPartially()
        {
            const string start_game = @"{""type"":""start_game"",""id"":1"
                + @",""names"":[""david"",""alice"",""brian"",""carol""]}";
            const string end_game = @"{""type"":""end_game""}";

            var conn = Substitute.For<ISimpleConnection>();
            conn.Receive().Returns(start_game, end_game);

            var factory = Substitute.For<IIntelligenceFactory>();
            factory.Create(null, null).ReturnsForAnyArgs(
                args => Substitute.For<Intelligence>(args.Arg<IntelligenceArgs>()));

            var spectator = new TestSpectator();

            MjaiClient.Play(conn, factory, new [] { spectator });

            conn.Received(1).Send(@"{""type"":""none""}");  // For start_game.
            conn.ReceivedWithAnyArgs(1).Send(null);

            factory.ReceivedWithAnyArgs(1).Create(null, null);

            Assert.AreEqual(1, spectator.NumRegisterHandlers);
            Assert.AreEqual(1, spectator.NumOnGameStarted);
            Assert.AreEqual(1, spectator.NumOnGameEnded);

            IGameState gameState = spectator.GetGameState();
            Assert.IsNotNull(gameState);
            Assert.AreEqual("alice", gameState.Players[0].Name);
            Assert.AreEqual(25000, gameState.Players[0].Score);
            Assert.AreEqual("brian", gameState.Players[1].Name);
            Assert.AreEqual(25000, gameState.Players[1].Score);
            Assert.AreEqual("carol", gameState.Players[2].Name);
            Assert.AreEqual(25000, gameState.Players[2].Score);
            Assert.AreEqual("david", gameState.Players[3].Name);
            Assert.AreEqual(25000, gameState.Players[3].Score);
        }
    }
}
