using System;
using System.Collections.Generic;

namespace LowCarb
{
    public class Wire
    {
        public Wire()
        {
            Init();
        }

        public Wire(PinHandle source, PinHandle destination)
        {
            Init();
            Source = source;
            Destination = destination;
        }

        public PinHandle Source
        {
            get
            {
                return m_source;
            }
            set
            {
                if (value.type != EPinType.Null)
                {
                    if (value.type != EPinType.Output)
                    {
                        throw new ArgumentException("Source pin must be an output pin or a null.");
                    }
                    if (value.hChip == m_destination.hChip &&
                        m_destination.type != EPinType.Null)
                    {
                        throw new ArgumentException("Source pin cannot be from the same chip as destination pin.");
                    }
                }

                m_source = value;
            }
        }

        public PinHandle Destination
        {
            get
            {
                return m_destination;
            }
            set
            {
                if (value.type != EPinType.Null)
                {
                    if (value.type != EPinType.Input)
                    {
                        throw new ArgumentException("Destination pin must be an input pin or null.");
                    }
                    if (value.hChip == m_source.hChip &&
                        m_source.type != EPinType.Null)
                    {
                        throw new ArgumentException("Destination pin cannot be from the same chip as source pin.");
                    }
                }

                m_destination = value;
            }
        }

        private void Init()
        {
            m_source = new PinHandle(UInt32.MaxValue, Int32.MaxValue, EPinType.Null);
            m_destination = new PinHandle(UInt32.MaxValue, Int32.MaxValue, EPinType.Null);
        }

        private PinHandle m_source;
        private PinHandle m_destination;
    }
}
