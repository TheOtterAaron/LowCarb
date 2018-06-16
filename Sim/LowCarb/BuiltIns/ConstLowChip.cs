using System.Collections.Generic;

namespace LowCarb.BuiltIns
{
    public class ConstLowChip : IChip
    {
        public IList<Pin> GetInputPins()
        {
            return new List<Pin>(0);
        }

        public IList<Pin> GetOutputPins()
        {
            return new List<Pin>() { new Pin("Out", ESignal.Low) };
        }

        public void Simulate()
        {
            return;
        }
    }
}
