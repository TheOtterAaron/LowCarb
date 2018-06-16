using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LowCarb;

namespace LowCarbTests
{
    [TestClass]
    public class WireTests
    {
        [TestMethod]
        public void TestWire()
        {
            Wire wire = new Wire(
                new PinHandle(0, 0, EPinType.Output),
                new PinHandle(1, 0, EPinType.Input));

            wire.Source = new PinHandle(2, 1, EPinType.Output);
            wire.Destination = new PinHandle(0, 0, EPinType.Input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWireSourceBadTypeException()
        {
            Wire wire = new Wire();
            wire.Source = new PinHandle(0, 0, EPinType.Input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWireSourceSameChipException()
        {
            Wire wire = new Wire();
            wire.Destination = new PinHandle(0, 0, EPinType.Input);
            wire.Source = new PinHandle(0, 0, EPinType.Output);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWireDestinationeBadTypeException()
        {
            Wire wire = new Wire();
            wire.Destination = new PinHandle(0, 0, EPinType.Output);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWireDestinationSameChipException()
        {
            Wire wire = new Wire(
                new PinHandle(0, 0, EPinType.Output),
                new PinHandle(0, 0, EPinType.Input));
        }
    }
}
