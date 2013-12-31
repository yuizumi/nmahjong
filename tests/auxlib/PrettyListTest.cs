using NUnit.Framework;
using System;

namespace NMahjong.Testing
{
    [TestFixture]
    public class TestHelperTest : TestHelper
    {
        [Test, TestCaseSource("TestToStringSource")]
        public void TestToString(int[] items, string expected)
        {
            Assert.AreEqual(expected, new PrettyList<Int32>(items).ToString());
        }

        #pragma warning disable 414
        // Putting PrettyList below is not a good idea -- NUnit generates
        // parameterized test names using ToString(), which is exactly
        // what we are going to test with the above code, and hence could
        // cause undesired results when ToString() behaves badly.
        private static readonly TestCaseData[]
        TestToStringSource = {
            Data(new int[] {}, "{}"),
            Data(new int[] {1}, "{1}"),
            Data(new int[] {1, 2, 3}, "{1, 2, 3}"),
        };
        #pragma warning restore 414
    }
}
