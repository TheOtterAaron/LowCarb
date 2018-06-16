using System.Collections.Generic;

namespace LowCarb.Validation
{
    public class ChipContract
    {
        public ChipContract(IList<ESignal> input, IList<ESignal> output)
        {
            m_input = new List<ESignal>(input);
            m_output = new List<ESignal>(output);
        }

        public ChipContract(IList<bool> input, IList<bool> output)
        {
            m_input = new List<ESignal>(input.Count);
            m_output = new List<ESignal>(output.Count);
            
            for (int i = 0; i < input.Count || i < output.Count; i++)
            {
                if (i < input.Count)
                {
                    m_input.Add(input[i] == false ? ESignal.Low : ESignal.High);
                }

                if (i < output.Count)
                {
                    m_output.Add(output[i] == false ? ESignal.Low : ESignal.High);
                }
            }
        }

        public ChipContract(IList<int> input, IList<int> output)
        {
            m_input = new List<ESignal>(input.Count);
            m_output = new List<ESignal>(output.Count);

            for (int i = 0; i < input.Count || i < output.Count; i++)
            {
                if (i < input.Count)
                {
                    m_input.Add(input[i] == 0 ? ESignal.Low : ESignal.High);
                }

                if (i < output.Count)
                {
                    m_output.Add(output[i] == 0 ? ESignal.Low : ESignal.High);
                }
            }
        }

        public bool Equals(ChipContract operand)
        {
            if (m_input.Count != operand.Input.Count ||
                m_output.Count != operand.Output.Count)
            {
                return false;
            }

            for (int i = 0; i < m_input.Count || i < m_output.Count; i++)
            {
                if (i < m_input.Count && m_input[i] != operand.Input[i])
                {
                    return false;
                }

                if (i < m_output.Count && m_output[i] != operand.Output[i])
                {
                    return false;
                }
            }

            return true;
        }

        public IList<ESignal> Input
        {
            get
            {
                return m_input.AsReadOnly();
            }
        }

        public IList<ESignal> Output
        {
            get
            {
                return m_output.AsReadOnly();
            }
        }

        private List<ESignal> m_input;
        private List<ESignal> m_output;
    }
}
