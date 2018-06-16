using System;
using System.Collections.Generic;

namespace LowCarb.BuiltIns
{
    public class ForwardChip : IChip
    {
        public ForwardChip()
        {
            m_in = new List<Pin>() { new Pin("In") };
            m_out = new List<Pin>() { new Pin("Out") };
        }

        public IList<Pin> GetInputPins()
        {
            return m_in.AsReadOnly();
        }

        public IList<Pin> GetOutputPins()
        {
            return m_out.AsReadOnly();
        }

        public void Simulate()
        {
            m_out[0] = m_in[0];
        }

        private List<Pin> m_in;
        private List<Pin> m_out;
    }
}
