using NUnit.Framework;
using NSubstitute;
using System;
using System.IO;
using System.Text;
using NMahjong.Testing;

namespace NMahjong.Aux.Tools
{
    [TestFixture]
    public class SubcommandDispatcherTest : CommandTestBase
    {
        private SubcommandDispatcher mDispatcher;
        private ISubcommand mFooCommand;
        private ISubcommand mBarCommand;

        private void SetupDispatcher()
        {
            mFooCommand = Substitute.For<ISubcommand>();
            mFooCommand.Name.Returns("foo");
            mBarCommand = Substitute.For<ISubcommand>();
            mBarCommand.Name.Returns("bar");
            mDispatcher = new SubcommandDispatcher(mFooCommand, mBarCommand);
            mDispatcher.ProgramName = "test.exe";
        }

        [Test, TestCaseSource("TestInvalidNames")]
        public void TestCtorInvalidName(string name)
        {
            var badCommand = Substitute.For<ISubcommand>();
            badCommand.Name.Returns(name);
            Assert.Throws<ArgumentException>(
                () => new SubcommandDispatcher(badCommand));
        }

        #pragma warning disable 414
        private static readonly string[] TestInvalidNames = {
            null, "", "help"
        };
        #pragma warning restore 414

        [Test]
        public void TestRun()
        {
            SetupDispatcher();
            string[] args = null;
            mBarCommand.Run(Arg.Do<String[]>(x => { args = x; }));
            Assert.AreEqual(0, mDispatcher.Run(new [] { "bar", "--quiet", "test" }));
            mFooCommand.DidNotReceiveWithAnyArgs().Run(null);
            mBarCommand.ReceivedWithAnyArgs(1).Run(null);
            Assert.AreEqual(new [] { "--quiet", "test" }, args);
        }

        [Test]
        public void TestRunHelp()
        {
            SetupDispatcher();
            Assert.AreEqual(0, mDispatcher.Run(new [] { "help" }));
            StringAssert.StartsWith("Usage:", ErrorOutput);
        }

        [Test]
        public void TestRunUnknownSubcommand()
        {
            SetupDispatcher();
            StringBuilder expectedOutput = new StringBuilder()
                .AppendLine("test.exe: Unknown subcommand -- xxx.")
                .AppendLine("test.exe: Type 'test.exe help' to show usage.");
            Assert.AreEqual(1, mDispatcher.Run(new [] { "xxx", "--quiet", "test" }));
            Assert.AreEqual(expectedOutput.ToString(), ErrorOutput);
        }
    }
}
