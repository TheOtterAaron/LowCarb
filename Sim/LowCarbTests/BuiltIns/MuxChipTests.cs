using Microsoft.VisualStudio.TestTools.UnitTesting;
using LowCarb.BuiltIns;
using LowCarb.Validation;

namespace LowCarbTests.BuiltIns
{
    [TestClass]
    public class MuxChipTests
    {
        [TestMethod]
        public void TestMuxChipContracts()
        {
            ChipValidator<MuxChip> chipValidator = new ChipValidator<MuxChip>();
            Assert.IsTrue(chipValidator.AddContracts(ChipContractsFileReader.ReadContracts("BuiltIns\\Contracts\\MuxChipContractsFile.txt")));
            Assert.IsTrue(chipValidator.Test());
        }
    }
}
