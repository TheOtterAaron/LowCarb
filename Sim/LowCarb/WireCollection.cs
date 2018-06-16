using System;
using System.Collections.Generic;

namespace LowCarb
{
    public delegate void WireHandler(Wire wire);

    public class WireCollection
    {
        public WireCollection()
        {
            m_wiresByPin = new Dictionary<uint, Dictionary<int, Wire>>();
        }

        public void Add(Wire wire)
        {
            if (Contains(wire.Destination))
            {
                throw new ArgumentException("Destination pin already occupied.");
            }

            uint hChip = wire.Destination.hChip;
            int index = wire.Destination.index;

            if (!m_wiresByPin.ContainsKey(wire.Destination.hChip))
            {
                m_wiresByPin.Add(hChip, new Dictionary<int, Wire>());
            }

            m_wiresByPin[hChip].Add(index, wire);
        }

        public bool Contains(PinHandle hPin)
        {
            return (hPin.type == EPinType.Input &&
                    m_wiresByPin.ContainsKey(hPin.hChip) &&
                    m_wiresByPin[hPin.hChip].ContainsKey(hPin.index));
        }

        public Wire At(PinHandle hPin)
        {
            if (!Contains(hPin))
            {
                throw new ArgumentException("Destination pin not found.");
            }

            return m_wiresByPin[hPin.hChip][hPin.index];
        }

        public bool Remove(PinHandle hPin)
        {
            bool exists = false;

            if (Contains(hPin))
            {
                exists = true;

                m_wiresByPin[hPin.hChip].Remove(hPin.index);
                if (m_wiresByPin[hPin.hChip].Count == 0)
                {
                    m_wiresByPin.Remove(hPin.hChip);
                }
            }

            return exists;
        }

        public void Foreach(WireHandler handler)
        {
            foreach (Dictionary<int, Wire> wires in m_wiresByPin.Values)
            {
                foreach (Wire wire in wires.Values)
                {
                    handler(wire);
                }
            }
        }

        private Dictionary<uint, Dictionary<int, Wire>> m_wiresByPin;
    }
}
