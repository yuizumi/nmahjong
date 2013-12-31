using NUnit.Framework;
using System.IO;
using NMahjong.Aux.Tools;

namespace NMahjong.Japanese.Drivers
{
    [TestFixture]
    public class IntelligenceOptionsExtensionTest
    {
        private IIntelligenceFactory GetFactory(string filename, string typeName)
        {
            return IntelligenceOptionsExtension.GetFactory(
                Path.Combine(TestContext.CurrentContext.TestDirectory, filename), typeName);
        }

        [Test]
        public void TestGetFactory()
        {
            IIntelligenceFactory factory = GetFactory(
                "NMahjong.Japanese.Drivers.Tests.dll", "DummyIntelligence");
            Assert.IsInstanceOf<DummyIntelligence>(factory);
        }

        [Test]
        public void TestGetFactoryBadFilename()
        {
            Assert.Throws<CommandLineException>(
                () => GetFactory("Nonexistent.dll", "DummyIntelligence"));
        }

        [Test]
        public void TestGetFactoryBadAssembly()
        {
            Assert.Throws<CommandLineException>(
                () => GetFactory(Path.Combine("testdata", "Empty.dll"), "DummyIntelligence"));
        }

        [Test]
        public void TestGetFactoryBadTypeName()
        {
            Assert.Throws<CommandLineException>(
                () => GetFactory("NMahjong.Japanese.Drivers.Tests.dll", "UnknownFactory"));
        }
    }
}
