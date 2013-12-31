using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace NMahjong.Testing
{
    public class TestHelper : AssertionHelper
    {
        public static TestCaseData Data(params object[] args)
        {
            return new TestCaseData(args);
        }

        public static IList<T> List<T>(params T[] items)
        {
            return new PrettyList<T>(items);
        }

        public static List<T> Enumerate<T>(IEnumerator<T> enumerator)
        {
            var list = new List<T>();
            using (enumerator) {
                while (enumerator.MoveNext()) list.Add(enumerator.Current);
            }
            return list;
        }

        public string GetTestDataDirectory()
        {
            return Path.Combine(TestContext.CurrentContext.TestDirectory, "testdata");
        }

        public string GetTestDataFile(string filename)
        {
            return Path.Combine(GetTestDataDirectory(), filename);
        }
    }
}
