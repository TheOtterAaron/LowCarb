using System.Collections.Generic;

namespace LowCarb
{
    public interface IChip
    {
        IList<Pin> GetInputPins();
        IList<Pin> GetOutputPins();
        void Simulate();
    }
}
