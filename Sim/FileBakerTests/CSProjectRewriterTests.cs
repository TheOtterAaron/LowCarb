using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileBaker;

namespace FileBakerTests
{
    [TestClass]
    public class CSProjectRewriterTests
    {
        [TestMethod]
        public void TestCSProjectRewriter()
        {
            File.Copy("TestFiles\\TestProject.csproj.txt", "CopyOfTestProject.csproj.txt");

            CSProjectRewriter.RewriteProject(
                "CopyOfTestProject.csproj.txt",
                "BuiltIns\\Contracts\\NotChipContractsFile.txt",
                EProjectFiletype.Contract);

            CSProjectRewriter.RewriteProject(
                "CopyOfTestProject.csproj.txt",
                "BuiltIns\\NotChipTests.cs",
                EProjectFiletype.UnitTest);

            string output = File.ReadAllText("CopyOfTestProject.csproj.txt");
            File.Delete("CopyOfTestProject.csproj.txt");

            string expectedOutput = File.ReadAllText("TestFiles\\ExpectedTestProject.csproj.txt");

            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void TestCSProjectRewriterFileNotFoundException()
        {
            CSProjectRewriter.RewriteProject("TestFiles\\MissingTestProject.csproj.txt", "foo", EProjectFiletype.Contract);
        }

        [TestMethod]
        [ExpectedException(typeof(XmlException))]
        public void TestCSProjectRewriterXmlException()
        {
            CSProjectRewriter.RewriteProject("TestFiles\\BadXmlTestProject.csproj.txt", "bar", EProjectFiletype.UnitTest);
        }
    }
}
