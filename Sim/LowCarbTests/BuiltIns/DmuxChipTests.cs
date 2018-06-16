using Microsoft.VisualStudio.TestTools.UnitTesting;
using LowCarb.BuiltIns;
using LowCarb.Validation;

namespace LowCarbTests.BuiltIns
{
    [TestClass]
    public class DmuxChipTests
    {
        [TestMethod]
        public void TestDmuxChipContracts()
        {
            ChipValidator<DmuxChip> chipValidator = new ChipValidator<DmuxChip>();
            Assert.IsTrue(chipValidator.AddContracts(ChipContractsFileReader.ReadContracts("BuiltIns\\Contracts\\DmuxChipContractsFile.txt")));
            Assert.IsTrue(chipValidator.Test());
        }
    }
}
