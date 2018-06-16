using System.Collections.Generic;

namespace LowCarb.BuiltIns
{
    public class DmuxChip : IChip
    {
        public DmuxChip()
        {
            AlphaLabelGenerator labelGenerator = new AlphaLabelGenerator();

            m_in = new List<Pin>()
            {
                new Pin("In"),
                new Pin("Sel")
            };

            m_out = new List<Pin>()
            {
                new Pin(labelGenerator),
                new Pin(labelGenerator)
            };
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
            if (m_in[0].Signal == ESignal.Unknown ||
                m_in[1].Signal == ESignal.Unknown)
            {
                m_out[0].Signal = ESignal.Unknown;
                m_out[1].Signal = ESignal.Unknown;
                return;
            }

            m_out[0].Signal = (m_in[1].Signal == ESignal.Low) ?
                               m_in[0].Signal : ESignal.Low;
            m_out[1].Signal = (m_in[1].Signal == ESignal.High) ?
                               m_in[0].Signal : ESignal.Low;
        }

        private List<Pin> m_in;
        private List<Pin> m_out;
    }
}
