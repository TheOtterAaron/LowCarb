using System.Collections.Generic;

namespace LowCarb.BuiltIns
{
    public class NotChip : IChip
    {
        public NotChip()
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
            switch (m_in[0].Signal)
            {
                case ESignal.Low:
                    m_out[0].Signal = ESignal.High;
                    break;
                case ESignal.High:
                    m_out[0].Signal = ESignal.Low;
                    break;
                default:
                    m_out[0].Signal = ESignal.Unknown;
                    break;
            }
        }

        private List<Pin> m_in;
        private List<Pin> m_out;
    }
}
