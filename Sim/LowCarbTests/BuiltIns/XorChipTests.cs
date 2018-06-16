using Microsoft.VisualStudio.TestTools.UnitTesting;
using LowCarb.BuiltIns;
using LowCarb.Validation;

namespace LowCarbTests.BuiltIns
{
    [TestClass]
    public class XorChipTests
    {
        [TestMethod]
        public void TestXorChipContracts()
        {
            ChipValidator<XorChip> chipValidator = new ChipValidator<XorChip>();
            Assert.IsTrue(chipValidator.AddContracts(ChipContractsFileReader.ReadContracts("BuiltIns\\Contracts\\XorChipContractsFile.txt")));
            Assert.IsTrue(chipValidator.Test());
        }
    }
}
