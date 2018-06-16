
namespace LowCarb
{
    public class Pin
    {
        public Pin(ESignal signal=ESignal.Unknown)
        {
            Label = "";
            Signal = signal;
        }

        public Pin(string label, ESignal signal=ESignal.Unknown)
        {
            Label = label;
            Signal = signal;
        }

        public Pin(ILabelGenerator labelGenerator, ESignal signal=ESignal.Unknown)
        {
            Label = labelGenerator.GenerateLabel();
            Signal = signal;
        }

        public string Label { get; set; }
        public ESignal Signal { get; set; }
    }
}
