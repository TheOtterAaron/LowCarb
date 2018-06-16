using Microsoft.VisualStudio.TestTools.UnitTesting;
using LowCarb;
using LowCarb.Validation;

namespace LowCarbTests.Validation
{
    [TestClass]
    public class ChipContractTests
    {
        [TestMethod]
        public void TestChipContractESignalConstructor()
        {
            ChipContract contract = new ChipContract(
                new ESignal[] { ESignal.High, ESignal.Low, ESignal.Low, ESignal.High },
                new ESignal[] { ESignal.Low, ESignal.High });
            Assert.AreEqual(ESignal.High, contract.Input[0]);
            Assert.AreEqual(ESignal.Low, contract.Input[1]);
            Assert.AreEqual(ESignal.Low, contract.Input[2]);
            Assert.AreEqual(ESignal.High, contract.Input[3]);
            Assert.AreEqual(ESignal.Low, contract.Output[0]);
            Assert.AreEqual(ESignal.High, contract.Output[1]);
        }

        [TestMethod]
        public void TestChipContractEquals()
        {
            ChipContract contractA = new ChipContract(
                new ESignal[] { ESignal.High, ESignal.Low },
                new ESignal[] { ESignal.High });

            ChipContract contractB = new ChipContract(
                new ESignal[] { ESignal.High, ESignal.Low },
                new ESignal[] { ESignal.High });

            ChipContract contractC = new ChipContract(
                new ESignal[] { ESignal.Low, ESignal.Low },
                new ESignal[] { ESignal.High });

            Assert.IsFalse(contractA == contractB);
            Assert.IsTrue(contractA.Equals(contractB));
            Assert.IsFalse(contractA.Equals(contractC));
        }

        [TestMethod]
        public void TestChipContractBoolConstructor()
        {
            ChipContract contract = new ChipContract(
                new bool[] { true, true, false, false },
                new bool[] { true, false });

            ChipContract expectedContract = new ChipContract(
                new ESignal[] { ESignal.High, ESignal.High, ESignal.Low, ESignal.Low },
                new ESignal[] { ESignal.High, ESignal.Low });

            Assert.IsTrue(expectedContract.Equals(contract));
        }

        [TestMethod]
        public void TestChipContractIntConstructor()
        {
            ChipContract contract = new ChipContract(
                new int[] { 10, 0, 1, 0 },
                new int[] { 1, 12, 0, 0 });

            ChipContract expectedContract = new ChipContract(
                new ESignal[] { ESignal.High, ESignal.Low, ESignal.High, ESignal.Low },
                new ESignal[] { ESignal.High, ESignal.High, ESignal.Low, ESignal.Low });

            Assert.IsTrue(expectedContract.Equals(contract));
        }

        [TestMethod]
        public void TestChipContractEmptyInput()
        {
            ChipContract contract = new ChipContract(
                new int[] { },
                new int[] { 1 });

            Assert.AreEqual(0, contract.Input.Count);
            Assert.AreEqual(ESignal.High, contract.Output[0]);
        }
    }
}
