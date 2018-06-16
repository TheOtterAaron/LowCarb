using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileBaker;
using LowCarb;

namespace FileBakerTests
{
    [TestClass]
    public class ChipSourceReaderTests
    {
        [TestMethod]
        public void TestChipSourceReader()
        {
            IChip stubChip = ChipSourceReader.ReadSource("TestFiles\\StubChip.cs.txt", "FileBakerTests.StubChip");
            Assert.AreNotEqual(null, stubChip);

            MethodInfo getInputPinsInfo = stubChip.GetType().GetMethod("GetInputPins");
            MethodInfo getOutputPinsInfo = stubChip.GetType().GetMethod("GetOutputPins");
            MethodInfo simulateInfo = stubChip.GetType().GetMethod("Simulate");
            Assert.AreNotEqual(null, getInputPinsInfo);
            Assert.AreNotEqual(null, getOutputPinsInfo);
            Assert.AreNotEqual(null, simulateInfo);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void TestChipSourceReaderFileNotFoundException()
        {
            ChipSourceReader.ReadSource("TestFiles\\MissingChip.cs.txt", "FileBakerTests.MissingChip");
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestChipSourceReaderApplicationException()
        {
            ChipSourceReader.ReadSource("TestFiles\\UncompilableChip.cs.txt", "FileBakerTests.UncompilableChip");
        }

        [TestMethod]
        [ExpectedException(typeof(TypeLoadException))]
        public void TestChipSourceReaderTypeLoadException()
        {
            ChipSourceReader.ReadSource("TestFiles\\StubChip.cs.txt", "FileBakerTests.MissingChip");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void TestChipSourceReaderInvalidCastException()
        {
            ChipSourceReader.ReadSource("TestFiles\\NonInterfacedChip.cs.txt", "FileBakerTests.NonInterfacedChip");
        }

        [TestMethod]
        [ExpectedException(typeof(MissingMethodException))]
        public void TestChipSourceReaderMissingMethodException()
        {
            ChipSourceReader.ReadSource("TestFiles\\UninstantiableChip.cs.txt", "FileBakerTests.UninstantiableChip");
        }
    }
}
