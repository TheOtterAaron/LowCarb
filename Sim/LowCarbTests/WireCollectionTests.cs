using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LowCarb;

namespace LowCarbTests
{
    [TestClass]
    public class WireCollectionTests
    {
        [TestMethod]
        public void TestWireCollectionAdd()
        {
            WireCollection wires = new WireCollection();

            wires.Add(new Wire(
                new PinHandle(0, 0, EPinType.Output),
                new PinHandle(1, 0, EPinType.Input)));

            Assert.AreEqual(true, wires.Contains(new PinHandle(1, 0, EPinType.Input)));
            Assert.AreEqual(false, wires.Contains(new PinHandle(1, 0, EPinType.Output)));
            Assert.AreEqual(false, wires.Contains(new PinHandle(1, 1, EPinType.Input)));
        }

        [TestMethod]
        public void TestWiresCollectionAt()
        {
            WireCollection wires = new WireCollection();

            wires.Add(new Wire(
                new PinHandle(0, 0, EPinType.Output),
                new PinHandle(1, 0, EPinType.Input)));

            Assert.AreEqual(
                new PinHandle(0, 0, EPinType.Output),
                wires.At(new PinHandle(1, 0, EPinType.Input)).Source);
        }

        [TestMethod]
        public void TestWireCollectionRemove()
        {
            WireCollection wires = new WireCollection();

            wires.Add(new Wire(
                new PinHandle(0, 0, EPinType.Output),
                new PinHandle(1, 0, EPinType.Input)));

            Assert.AreEqual(true, wires.Remove(new PinHandle(1, 0, EPinType.Input)));
            Assert.AreEqual(false, wires.Remove(new PinHandle(1, 0, EPinType.Input)));
        }

        [TestMethod]
        public void TestWiresCollectionForeach()
        {
            WireCollection wires = new WireCollection();

            wires.Add(new Wire(
                new PinHandle(0, 0, EPinType.Output),
                new PinHandle(1, 0, EPinType.Input)));

            wires.Add(new Wire(
                new PinHandle(2, 0, EPinType.Output),
                new PinHandle(3, 0, EPinType.Input)));

            wires.Add(new Wire(
                new PinHandle(4, 0, EPinType.Output),
                new PinHandle(5, 0, EPinType.Input)));

            wires.Add(new Wire(
                new PinHandle(6, 0, EPinType.Output),
                new PinHandle(7, 0, EPinType.Input)));

            uint i = 0;
            wires.Foreach(delegate (Wire wire)
            {
                Assert.AreEqual(i, wire.Source.hChip);
                i += 2;
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWiresCollectionPinOccupiedException()
        {
            WireCollection wires = new WireCollection();

            wires.Add(new Wire(
                new PinHandle(0, 0, EPinType.Output),
                new PinHandle(1, 0, EPinType.Input)));

            wires.Add(new Wire(
                new PinHandle(0, 0, EPinType.Output),
                new PinHandle(1, 0, EPinType.Input)));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWiresCollectionPinNotFoundException()
        {
            WireCollection wires = new WireCollection();

            wires.Add(new Wire(
                new PinHandle(0, 0, EPinType.Output),
                new PinHandle(1, 0, EPinType.Input)));

            wires.At(new PinHandle(0, 0, EPinType.Output));
        }
    }
}
