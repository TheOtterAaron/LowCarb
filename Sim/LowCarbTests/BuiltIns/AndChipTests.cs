using Microsoft.VisualStudio.TestTools.UnitTesting;
using LowCarb.BuiltIns;
using LowCarb.Validation;

namespace LowCarbTests.BuiltIns
{
    [TestClass]
    public class AndChipTests
    {
        [TestMethod]
        public void TestAndChipContracts()
        {
            ChipValidator<AndChip> chipValidator = new ChipValidator<AndChip>();
            Assert.IsTrue(chipValidator.AddContracts(ChipContractsFileReader.ReadContracts("BuiltIns\\Contracts\\AndChipContractsFile.txt")));
            Assert.IsTrue(chipValidator.Test());
        }
    }
}
