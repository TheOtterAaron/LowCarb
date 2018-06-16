using Microsoft.VisualStudio.TestTools.UnitTesting;
using LowCarb.BuiltIns;
using LowCarb.Validation;

namespace LowCarbTests.BuiltIns
{
    [TestClass]
    public class ConstLowChipTests
    {
        [TestMethod]
        public void TestConstLowChipContracts()
        {
            ChipValidator<ConstLowChip> chipValidator = new ChipValidator<ConstLowChip>();
            Assert.IsTrue(chipValidator.AddContracts(ChipContractsFileReader.ReadContracts("BuiltIns\\Contracts\\ConstLowChipContractsFile.txt")));
            Assert.IsTrue(chipValidator.Test());
        }
    }
}
