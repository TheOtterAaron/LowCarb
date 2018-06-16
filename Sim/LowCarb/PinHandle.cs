
namespace LowCarb
{
    public struct PinHandle
    {
        public PinHandle(uint hChip, int index, EPinType type)
        {
            this.hChip = hChip;
            this.index = index;
            this.type = type;
        }

        public uint hChip;
        public int index;
        public EPinType type;

        public override bool Equals(object obj)
        {
            return obj is PinHandle && this == (PinHandle)obj;
        }

        public override int GetHashCode()
        {
            return hChip.GetHashCode() ^
                    index.GetHashCode() ^
                    type.GetHashCode();
        }

        public static bool operator ==(PinHandle x, PinHandle y)
        {
            return (x.hChip == y.hChip &&
                    x.index == y.index &&
                    x.type == y.type);
        }

        public static bool operator !=(PinHandle x, PinHandle y)
        {
            return !(x == y);
        }
    }
}
