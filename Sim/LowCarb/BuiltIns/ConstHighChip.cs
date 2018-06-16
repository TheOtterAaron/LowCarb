using System.Collections.Generic;

namespace LowCarb.BuiltIns
{
    public class ConstHighChip : IChip
    {
        public IList<Pin> GetInputPins()
        {
            return new List<Pin>(0);
        }

        public IList<Pin> GetOutputPins()
        {
            return new List<Pin>() { new Pin("Out", ESignal.High) };
        }

        public void Simulate()
        {
            return;
        }
    }
}
