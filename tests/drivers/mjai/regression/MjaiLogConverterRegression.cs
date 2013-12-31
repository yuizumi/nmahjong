using NUnit.Framework;
using System.IO;
using Newtonsoft.Json.Linq;
using NMahjong.Testing;

namespace NMahjong.Drivers.Mjai
{
    [TestFixture, Category("Regression")]
    public class MjaiLogConverterRegression : TestHelper
    {
        [TestCase("2013-10-19-183731.mjson", "2013-10-19-183731.nmj.json")]
        [TestCase("2013-12-30-170956.mjson", "2013-12-30-170956.nmj.json")]
        public void TestIt(string mjsonFile, string nmjFile)
        {
            string nmjText = File.ReadAllText(GetTestDataFile(nmjFile));
            JArray golden = JArray.Parse(nmjText);

            StringWriter writer = new StringWriter();
            using (var reader = new StreamReader(GetTestDataFile(mjsonFile))) {
                MjaiLogConverter.Convert(reader, writer);
            }
            JArray actual = JArray.Parse(writer.ToString());

            TweakForApiChanges(golden, actual);
            // TODO(yuizumi): Write a dedicated assert method.
            Assert.IsTrue(JToken.DeepEquals(golden, actual));
        }

        private static void TweakForApiChanges(JArray golden, JArray actual)
        {
        }
    }
}
