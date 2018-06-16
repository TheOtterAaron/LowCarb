using Microsoft.VisualStudio.TestTools.UnitTesting;
using LowCarb.BuiltIns;
using LowCarb.Validation;

namespace LowCarbTests.BuiltIns
{
    [TestClass]
    public class NandChipTests
    {
        [TestMethod]
        public void TestNandChipContracts()
        {
            ChipValidator<NandChip> chipValidator = new ChipValidator<NandChip>();
            Assert.IsTrue(chipValidator.AddContracts(ChipContractsFileReader.ReadContracts("BuiltIns\\Contracts\\NandChipContractsFile.txt")));
            Assert.IsTrue(chipValidator.Test());
        }
    }
}
