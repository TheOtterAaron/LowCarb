using Microsoft.VisualStudio.TestTools.UnitTesting;
using LowCarb;

namespace LowCarbTests
{
    [TestClass]
    public class PinTests
    {
        [TestMethod]
        public void TestPinConstructor()
        {
            Pin pin = new Pin();
            Assert.AreEqual("", pin.Label);
            Assert.AreEqual(ESignal.Unknown, pin.Signal);

            Pin highPin = new Pin(ESignal.High);
            Assert.AreEqual(ESignal.High, highPin.Signal);

            Pin labeledPin = new Pin("In");
            Assert.AreEqual("In", labeledPin.Label);
            Assert.AreEqual(ESignal.Unknown, labeledPin.Signal);

            Pin autoLabeledPin = new Pin(new AlphaLabelGenerator(), ESignal.Low);
            Assert.AreEqual("A", autoLabeledPin.Label);
            Assert.AreEqual(ESignal.Low, autoLabeledPin.Signal);
        }

        [TestMethod]
        public void TestPinLabelMutator()
        {
            Pin pin = new Pin();
            pin.Label = "Out";
            Assert.AreEqual("Out", pin.Label);
        }

        [TestMethod]
        public void TestPinSignalMutator()
        {
            Pin pin = new Pin();
            pin.Signal = ESignal.High;
            Assert.AreEqual(ESignal.High, pin.Signal);
        }
    }
}
