using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LowCarb;
using LowCarb.BuiltIns;

namespace LowCarbTests
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void TestBoardGetChip()
        {
            Board board = new Board();
            uint hAnd = board.AddChip<AndChip>();

            IChip and = board.GetChip(hAnd);
            Assert.IsNotNull(and);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBoardRemoveChip()
        {
            Board board = new Board();
            uint hAnd = board.AddChip<AndChip>();

            Assert.AreEqual(true, board.RemoveChip(hAnd));
            Assert.AreEqual(false, board.RemoveChip(hAnd));

            board.GetChip(hAnd);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBoardGetPinArgumentException()
        {
            Board board = new Board();

            uint hAnd = board.AddChip<AndChip>();
            board.RemoveChip(hAnd);

            board.GetPin(new PinHandle(hAnd, 1, EPinType.Input));
        }

        [TestMethod]
        public void TestBoardStepSimulation()
        {
            Board board = new Board();

            uint hHigh = board.AddChip<ConstHighChip>();
            uint hFwd = board.AddChip<ForwardChip>();
            uint hNot = board.AddChip<NotChip>();

            IChip high = board.GetChip(hHigh);
            IChip fwd = board.GetChip(hFwd);
            IChip not = board.GetChip(hNot);

            board.ConnectPins(
                new PinHandle(hHigh, 0, EPinType.Output),
                new PinHandle(hFwd, 0, EPinType.Input));

            board.ConnectPins(
                new PinHandle(hFwd, 0, EPinType.Output),
                new PinHandle(hNot, 0, EPinType.Input));

            Assert.AreEqual(ESignal.Unknown, fwd.GetInputPins()[0].Signal);

            board.StepSimulation();
            Assert.AreEqual(ESignal.High, fwd.GetInputPins()[0].Signal);
            Assert.AreEqual(ESignal.Unknown, not.GetInputPins()[0].Signal);

            board.StepSimulation();
            Assert.AreEqual(ESignal.High, not.GetInputPins()[0].Signal);
            Assert.AreEqual(ESignal.Low, not.GetOutputPins()[0].Signal);
            
        }

        [TestMethod]
        public void TestBoardWithTriAnd()
        {
            Board board = new Board();

            uint hLow = board.AddChip<ConstLowChip>();
            uint hHigh = board.AddChip<ConstHighChip>();
            uint hAndChip1 = board.AddChip<AndChip>();
            uint hAndChip2 = board.AddChip<AndChip>();
            
            Pin output = board.GetPin(new PinHandle(hAndChip2, 0, EPinType.Output));
            
            // Build the circuit
            board.ConnectPins(
                new PinHandle(hAndChip1, 0, EPinType.Output),
                new PinHandle(hAndChip2, 0, EPinType.Input));

            // Set Inputs [0, 1, 1]
            board.ConnectPins(
                new PinHandle(hLow, 0, EPinType.Output),
                new PinHandle(hAndChip1, 0, EPinType.Input));

            board.ConnectPins(
                new PinHandle(hHigh, 0, EPinType.Output),
                new PinHandle(hAndChip1, 1, EPinType.Input));

            board.ConnectPins(
                new PinHandle(hHigh, 0, EPinType.Output),
                new PinHandle(hAndChip2, 1, EPinType.Input));

            // Solve (0)
            board.SolveSimulation();
            Assert.AreEqual(ESignal.Low, output.Signal);

            // Adjust Inputs [1, 1, 1]
            board.ConnectPins(
                new PinHandle(hHigh, 0, EPinType.Output),
                new PinHandle(hAndChip1, 0, EPinType.Input));

            // Solve (1)
            board.SolveSimulation();
            Assert.AreEqual(ESignal.High, output.Signal);
        }

        [TestMethod]
        public void TestBoardWithXOR()
        {
            Board board = new Board();

            uint hLow = board.AddChip<ConstLowChip>();
            uint hHigh = board.AddChip<ConstHighChip>();
            uint hAndChip1 = board.AddChip<AndChip>();
            uint hAndChip2 = board.AddChip<AndChip>();
            uint hNotChip1 = board.AddChip<NotChip>();
            uint hNotChip2 = board.AddChip<NotChip>();
            uint hOrChip = board.AddChip<OrChip>();
            uint hForwardChip1 = board.AddChip<ForwardChip>();
            uint hForwardChip2 = board.AddChip<ForwardChip>();

            Pin output = board.GetPin(new PinHandle(hOrChip, 0, EPinType.Output));

            // Build Circuit
            board.ConnectPins(
                new PinHandle(hForwardChip1, 0, EPinType.Output),
                new PinHandle(hAndChip1, 0, EPinType.Input));

            board.ConnectPins(
                new PinHandle(hForwardChip2, 0, EPinType.Output),
                new PinHandle(hNotChip1, 0, EPinType.Input));

            board.ConnectPins(
                new PinHandle(hNotChip1, 0, EPinType.Output),
                new PinHandle(hAndChip1, 1, EPinType.Input));

            board.ConnectPins(
                new PinHandle(hForwardChip2, 0, EPinType.Output),
                new PinHandle(hAndChip2, 0, EPinType.Input));

            board.ConnectPins(
                new PinHandle(hForwardChip1, 0, EPinType.Output),
                new PinHandle(hNotChip2, 0, EPinType.Input));

            board.ConnectPins(
                new PinHandle(hNotChip2, 0, EPinType.Output),
                new PinHandle(hAndChip2, 1, EPinType.Input));

            board.ConnectPins(
                new PinHandle(hAndChip1, 0, EPinType.Output),
                new PinHandle(hOrChip, 0, EPinType.Input));

            board.ConnectPins(
                new PinHandle(hAndChip2, 0, EPinType.Output),
                new PinHandle(hOrChip, 1, EPinType.Input));

            // Set Inputs [0, -]
            board.ConnectPins(
                new PinHandle(hLow, 0, EPinType.Output),
                new PinHandle(hForwardChip1, 0, EPinType.Input));

            // Solve (Unknown)
            board.SolveSimulation();
            Assert.AreEqual(ESignal.Unknown, output.Signal);

            // Adjust Inputs [0, 1]
            board.ConnectPins(
                new PinHandle(hHigh, 0, EPinType.Output),
                new PinHandle(hForwardChip2, 0, EPinType.Input));

            // Solve (1)
            board.SolveSimulation();
            Assert.AreEqual(ESignal.High, output.Signal);

            // Adjust Inputs [-, 1]
            board.DisconnectPins(
                new PinHandle(hForwardChip1, 0, EPinType.Input));

            // Solve (Unknown)
            board.SolveSimulation();
            Assert.AreEqual(ESignal.Unknown, output.Signal);

            // Adjust Inputs [1, 1]
            board.ConnectPins(
                new PinHandle(hHigh, 0, EPinType.Output),
                new PinHandle(hForwardChip1, 0, EPinType.Input));

            // Solve (0)
            board.SolveSimulation();
            Assert.AreEqual(ESignal.Low, output.Signal);
        }
    }
}
