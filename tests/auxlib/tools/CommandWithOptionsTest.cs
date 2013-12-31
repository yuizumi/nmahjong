using NUnit.Framework;
using System;
using System.IO;
using Mono.Options;

namespace NMahjong.Aux.Tools
{
    [TestFixture]
    public class CommandWithOptionsTest : CommandTestBase
    {
        private class FooOptions : OptionSetProvider
        {
            public string FooArg
            {
                get; private set;
            }

            public bool Complete
            {
                get; private set;
            }

            public override OptionSet GetOptionSet()
            {
                return new OptionSet() { {"foo=", (arg => { FooArg = arg; })} };
            }

            public override void OnParseComplete()
            {
                Complete = true;
            }
        }

        private class FooCommand : CommandWithOptions<FooOptions>
        {
            private string mErrorMessage;

            public string[] Args
            {
                get; private set;
            }

            public FooOptions Options
            {
                get; private set;
            }

            public void SetErrorMessage(string message)
            {
                mErrorMessage = message;
            }

            protected override void Run(string[] args, FooOptions options)
            {
                Args = args;
                Options = options;
                if (mErrorMessage != null) throw new CommandLineException(mErrorMessage);
            }
        }

        private FooCommand mCommand;

        private static readonly string NL = Environment.NewLine;

        [SetUp]
        public void Setup()
        {
            mCommand = new FooCommand();
            mCommand.ProgramName = "test.exe";
        }

        [Test]
        public void TestWithoutOption()
        {
            Assert.AreEqual(0, mCommand.Run(new string[] { "alpha", "bravo" }));
            Assert.IsNull(mCommand.Options.FooArg);
            Assert.IsTrue(mCommand.Options.Complete);
            Assert.AreEqual(new [] { "alpha", "bravo" }, mCommand.Args);
        }

        [Test]
        public void TestWithOption()
        {
            Assert.AreEqual(0, mCommand.Run(new string[] { "--foo", "alpha", "bravo" }));
            Assert.AreEqual("alpha", mCommand.Options.FooArg);
            Assert.IsTrue(mCommand.Options.Complete);
            Assert.AreEqual(new [] { "bravo" },mCommand.Args);
        }

        [Test]
        public void TestFailure()
        {
            mCommand.SetErrorMessage("I hate you.");
            Assert.AreEqual(1, mCommand.Run(new string[] { "alpha", "bravo" }));
            Assert.AreEqual("test.exe: I hate you." + NL, ErrorOutput);
        }

        [Test]
        public void TestHelpOption()
        {
            Assert.AreEqual(0, mCommand.Run(new string[] { "--help" }));
            StringAssert.StartsWith("Usage: test.exe [OPTIONS]" + NL, ErrorOutput);
        }

        [Test]
        public void TestInvalidOption()
        {
            Assert.AreEqual(1, mCommand.Run(new string[] { "--foo" }));
            StringAssert.StartsWith("test.exe: ", ErrorOutput);
        }
    }
}
