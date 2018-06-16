using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LowCarb.BuiltIns;
using FileBaker;

namespace FileBakerTests
{
    [TestClass]
    public class ChipContractsFileWriterTests
    {
        [TestMethod]
        public void TestChipContractsFileWriter()
        {
            ChipContractsFileWriter.WriteFile("AndChipContractsFile.txt", new AndChip());

            string[] output = File.ReadAllLines("AndChipContractsFile.txt");
            File.Delete("AndChipContractsFile.txt");

            string[] expectedOutput =
            {
                "// LowCarb.BuiltIns.AndChip",
                "// ",
                "// A  B  |  Out",
                "   0  0  |  ",
                "   0  1  |  ",
                "   1  0  |  ",
                "   1  1  |  "
            };

            for (int i = 0; i < output.Length || i < expectedOutput.Length; i++)
            {
                Assert.AreEqual(expectedOutput[i], output[i]);
            }
        }
    }
}
