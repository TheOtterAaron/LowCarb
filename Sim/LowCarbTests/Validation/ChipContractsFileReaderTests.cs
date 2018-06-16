using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using LowCarb.Validation;

namespace LowCarbTests.Validation
{
    [TestClass]
    public class ChipContractsFileReaderTests
    {
        [TestMethod]
        public void TestChipContractsFileReader()
        {
            IList<ChipContract> contracts = ChipContractsFileReader.ReadContracts("Validation\\TestContractsFile.txt");

            ChipContract[] expectedContracts = new ChipContract[]
            {
                new ChipContract(new int[] { 0, 0 }, new int[] { 0 }),
                new ChipContract(new int[] { 0, 1 }, new int[] { 0 }),
                new ChipContract(new int[] { 1, 0 }, new int[] { 0 }),
                new ChipContract(new int[] { 1, 1 }, new int[] { 1 }),
                new ChipContract(new int[] { }, new int[] { 1 })
            };

            for (int i = 0; i < contracts.Count; i++)
            {
                Assert.IsTrue(expectedContracts[i].Equals(contracts[i]));
            }
        }
    }
}
