using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using NMahjong.Base;
using NMahjong.Testing;

namespace NMahjong.Sample
{
    [TestFixture]
    public class ShantenHelperTest : TestHelper
    {
        // shanten_benchmark_data.num.txt was taken from github.com:gimite/mjai.
        [TestCase("shanten_benchmark_data.num.txt")]
        [TestCase("shanten_edgecases_data.num.txt")]
        public void TestGetShantensu(string filename)
        {
            using (var reader = new StreamReader(GetTestDataFile(filename))) {
                while (true) {
                    string line = reader.ReadLine();
                    if (line == null) break;
                    int[] row = line.Split(' ').Select(Int32.Parse).ToArray();
                    int[] vector = new int[Tile.AllTiles.Count];
                    for (int i = 0; i < row.Length - 1; i++) ++vector[row[i]];
                    int expected = row[row.Length - 1];
                    Assert.AreEqual(
                        expected, ShantenHelper.GetShantensu(vector, 4), "Failed with: " + line);
                }
            }
        }
    }
}
