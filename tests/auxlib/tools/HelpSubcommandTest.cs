using NUnit.Framework;
using NSubstitute;
using System.IO;
using System.Text;

namespace NMahjong.Aux.Tools
{
    [TestFixture]
    public class HelpSubcommandTest : CommandTestBase
    {
        [Test]
        public void TestRun()
        {
            var subcommands = new SubcommandSet();

            var dummy = Substitute.For<ISubcommand>();
            dummy.Description.Returns("Do nothing.");
            dummy.Name.Returns("dummy");
            subcommands.Add(dummy);

            var help = new HelpSubcommand(subcommands);
            help.ProgramName = "test.exe";
            subcommands.Add(help);

            StringBuilder expectedOutput = new StringBuilder()
                .AppendLine("Usage: test.exe SUBCOMMAND ...")
                .AppendLine()
                .AppendLine("  dummy    Do nothing.")
                .AppendLine("  help     Print this message.");

            Assert.AreEqual(0, help.Run(new string[0]));
            Assert.AreEqual(expectedOutput.ToString(), ErrorOutput);
        }
    }
}
