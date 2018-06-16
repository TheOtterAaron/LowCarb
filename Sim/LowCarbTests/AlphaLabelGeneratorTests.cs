using Microsoft.VisualStudio.TestTools.UnitTesting;
using LowCarb;

namespace LowCarbTests
{
    [TestClass]
    public class AlphaLabelGeneratorTests
    {
        [TestMethod]
        public void TestAlphaLabelGeneratorLabels()
        {
            ILabelGenerator labelGenerator = new AlphaLabelGenerator();
            Assert.AreEqual("A", labelGenerator.GenerateLabel());

            for (int i = 0; i < 24; i++)
            {
                labelGenerator.GenerateLabel();
            }

            Assert.AreEqual("Z", labelGenerator.GenerateLabel());
            Assert.AreEqual("AA", labelGenerator.GenerateLabel());
            Assert.AreEqual("BB", labelGenerator.GenerateLabel());
        }
    }
}
