using Microsoft.VisualStudio.TestTools.UnitTesting;
using LowCarb.BuiltIns;
using LowCarb.Validation;

namespace LowCarbTests.BuiltIns
{
    [TestClass]
    public class NotChipTests
    {
        [TestMethod]
        public void TestNotChipContracts()
        {
            ChipValidator<NotChip> chipValidator = new ChipValidator<NotChip>();
            Assert.IsTrue(chipValidator.AddContracts(ChipContractsFileReader.ReadContracts("BuiltIns\\Contracts\\NotChipContractsFile.txt")));
            Assert.IsTrue(chipValidator.Test());
        }
    }
}
