using Microsoft.VisualStudio.TestTools.UnitTesting;
using LowCarb.BuiltIns;
using LowCarb.Validation;

namespace LowCarbTests.BuiltIns
{
    [TestClass]
    public class OrChipTests
    {
        [TestMethod]
        public void TestOrChipContracts()
        {
            ChipValidator<OrChip> chipValidator = new ChipValidator<OrChip>();
            Assert.IsTrue(chipValidator.AddContracts(ChipContractsFileReader.ReadContracts("BuiltIns\\Contracts\\OrChipContractsFile.txt")));
            Assert.IsTrue(chipValidator.Test());
        }
    }
}
