using Microsoft.VisualStudio.TestTools.UnitTesting;
using LowCarb.BuiltIns;
using LowCarb.Validation;

namespace LowCarbTests.BuiltIns
{
    [TestClass]
    public class ConstHighChipTests
    {
        [TestMethod]
        public void TestConstHighChipContracts()
        {
            ChipValidator<ConstHighChip> chipValidator = new ChipValidator<ConstHighChip>();
            Assert.IsTrue(chipValidator.AddContracts(ChipContractsFileReader.ReadContracts("BuiltIns\\Contracts\\ConstHighChipContractsFile.txt")));
            Assert.IsTrue(chipValidator.Test());
        }
    }
}
