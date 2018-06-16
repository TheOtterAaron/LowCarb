using System.Collections.Generic;

namespace LowCarb.Validation
{
    public class ChipValidator<C> where C : IChip, new()
    {
        public ChipValidator()
        {
            m_chip = new C();
            m_contracts = new List<ChipContract>();
        }

        public bool AddContract(ChipContract contract)
        {
            if (m_chip.GetInputPins().Count != contract.Input.Count)
            {
                return false;
            }

            if (m_chip.GetOutputPins().Count != contract.Output.Count)
            {
                return false;
            }

            m_contracts.Add(contract);
            return true;
        }

        public bool AddContracts(IList<ChipContract> contracts)
        {
            bool result = true;

            foreach (ChipContract contract in contracts)
            {
                result &= AddContract(contract);
            }

            return result;
        }

        public bool Test()
        {
            foreach (ChipContract contract in m_contracts)
            {
                IList<Pin> inputPins = m_chip.GetInputPins();
                for (int i = 0; i < inputPins.Count; i++)
                {
                    inputPins[i].Signal = contract.Input[i];
                }

                m_chip.Simulate();

                IList<Pin> outputPins = m_chip.GetOutputPins();
                for (int i = 0; i < outputPins.Count; i++)
                {
                    if (contract.Output[i] != outputPins[i].Signal)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private C m_chip;
        private List<ChipContract> m_contracts;
    }
}
