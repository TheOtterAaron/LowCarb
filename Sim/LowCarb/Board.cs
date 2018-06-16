using System;
using System.Collections.Generic;

namespace LowCarb
{
    public class Board
    {
        public Board()
        {
            m_nextHandle = 0;
            m_chipsByHandle = new Dictionary<uint, IChip>();
            m_wires = new WireCollection();
        }
        
        public uint AddChip<C>() where C : IChip, new()
        {
            IChip chip = new C();
            uint hChip = m_nextHandle;

            m_chipsByHandle.Add(hChip, chip);
            m_nextHandle++;

            for (int i = 0; i < chip.GetInputPins().Count; i++)
            {
                m_wires.Add(new Wire(
                    new PinHandle()
                    {
                        type = EPinType.Null
                    },
                    new PinHandle(hChip, i, EPinType.Input)));
            }

            return hChip;
        }

        public bool ContainsChip(uint hChip)
        {
            return m_chipsByHandle.ContainsKey(hChip);
        }

        public IChip GetChip(uint hChip)
        {
            IChip chip = null;

            if (ContainsChip(hChip))
            {
                chip = m_chipsByHandle[hChip];
            }
            else
            {
                throw new ArgumentException("Invalid chip handle.");
            }

            return chip;
        }

        public bool RemoveChip(uint hChip)
        {
            bool exists = false;

            if (ContainsChip(hChip))
            {
                exists = true;

                for (int i = 0; i < m_chipsByHandle[hChip].GetInputPins().Count; i++)
                {
                    m_wires.Remove(new PinHandle(hChip, i, EPinType.Input));
                }
                m_chipsByHandle.Remove(hChip);
            }

            return exists;
        }

        public bool ContainsPin(PinHandle hPin)
        {
            bool exists = false;

            if (ContainsChip(hPin.hChip) && hPin.index >= 0)
            {
                if (hPin.type == EPinType.Input)
                {
                    exists = (hPin.index < m_chipsByHandle[hPin.hChip].GetInputPins().Count);
                }
                else if (hPin.type == EPinType.Output)
                {
                    exists = (hPin.index < m_chipsByHandle[hPin.hChip].GetOutputPins().Count);
                }
            }

            return exists;
        }

        public Pin GetPin(PinHandle hPin)
        {
            Pin pin = null;

            if (ContainsPin(hPin))
            {
                if (hPin.type == EPinType.Input)
                {
                    pin = m_chipsByHandle[hPin.hChip].GetInputPins()[hPin.index];
                }
                else if (hPin.type == EPinType.Output)
                {
                    pin = m_chipsByHandle[hPin.hChip].GetOutputPins()[hPin.index];
                }
            }
            else
            {
                throw new ArgumentException("Invalid pin handle.");
            }

            return pin;
        }

        public void ConnectPins(PinHandle hSrcPin, PinHandle hDstPin)
        {
            if (!ContainsPin(hSrcPin))
            {
                throw new ArgumentException("Invalid source pin handle.");
            }
            if (!ContainsPin(hDstPin))
            {
                throw new ArgumentException("Invalid destination pin handle.");
            }

            try
            {
                m_wires.At(hDstPin).Source = hSrcPin;
            }
            catch (ArgumentException e)
            {
                throw e;
            }
        }

        public void DisconnectPins(PinHandle hDstPin)
        {
            if (!ContainsPin(hDstPin))
            {
                throw new ArgumentException("Invalid destination pin handle.");
            }

            m_wires.At(hDstPin).Source = new PinHandle() { type = EPinType.Null };
        }

        public 

        public bool StepSimulation()
        {
            Queue<uint> chipsToSimulate = new Queue<uint>();

            m_wires.Foreach(delegate (Wire wire)
            {
                ESignal receivedSignal = ESignal.Unknown;

                if (ContainsPin(wire.Source))
                {
                    receivedSignal = GetPin(wire.Source).Signal;  
                }

                Pin dst = GetPin(wire.Destination);
                if (dst.Signal != receivedSignal)
                {
                    dst.Signal = receivedSignal;
                    chipsToSimulate.Enqueue(wire.Destination.hChip);
                }
            });

            int chipsSimulated = 0;
            while (chipsToSimulate.Count > 0)
            {
                m_chipsByHandle[chipsToSimulate.Dequeue()].Simulate();
                chipsSimulated++;
            }

            return chipsSimulated > 0;
        }

        public void SolveSimulation()
        {
            while (StepSimulation())
            {
                continue;
            }
        }

        private uint m_nextHandle;
        private Dictionary<uint, IChip> m_chipsByHandle;
        private WireCollection m_wires;
    }
}
