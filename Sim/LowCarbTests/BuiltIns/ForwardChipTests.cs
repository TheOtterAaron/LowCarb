using Microsoft.VisualStudio.TestTools.UnitTesting;
using LowCarb.BuiltIns;
using LowCarb.Validation;

namespace LowCarbTests.BuiltIns
{
    [TestClass]
    public class ForwardChipTests
    {
        [TestMethod]
        public void TestForwardChipContracts()
        {
            ChipValidator<ForwardChip> chipValidator = new ChipValidator<ForwardChip>();
            Assert.IsTrue(chipValidator.AddContracts(ChipContractsFileReader.ReadContracts("BuiltIns\\Contracts\\ForwardChipContractsFile.txt")));
            Assert.IsTrue(chipValidator.Test());
        }
    }
}
