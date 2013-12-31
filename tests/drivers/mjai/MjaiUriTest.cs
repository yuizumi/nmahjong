using NUnit.Framework;
using System;
using NMahjong.Testing;

namespace NMahjong.Drivers.Mjai
{
    [TestFixture]
    public class MjaiUriTest : TestHelper
    {
        [Test]
        public void TestParseWithoutSlash()
        {
            MjaiUri uri = MjaiUri.Parse("mjsonp://localhost:11600/default");
            Assert.AreEqual("localhost", uri.Host);
            Assert.AreEqual(11600, uri.Port);
            Assert.AreEqual("default", uri.Room);
        }

        [Test]
        public void TestParseWithSlash()
        {
            MjaiUri uri = MjaiUri.Parse("mjsonp://localhost:11600/foo/bar");
            Assert.AreEqual("localhost", uri.Host);
            Assert.AreEqual(11600, uri.Port);
            Assert.AreEqual("foo/bar", uri.Room);
        }

        [Test, TestCaseSource("TestParseInvalidSource")]
        public void TestParseInvalid(string uriString)
        {
            Assert.Throws<ArgumentException>(() => MjaiUri.Parse(uriString));
        }

        #pragma warning disable 414
        private static readonly TestCaseData[]
        TestParseInvalidSource = {
            // Invalid scheme.
            Data("http://localhost:11600/default"),
            // No port number.
            Data("mjsonp://localhost/default"),
            // Query or fragment.
            Data("mjsonp://localhost:11600/default?foo=bar"),
            Data("mjsonp://localhost:11600/default#foo-bar"),
        };
        #pragma warning restore 414
    }
}
