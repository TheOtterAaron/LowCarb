
namespace LowCarb
{
    public class AlphaLabelGenerator : ILabelGenerator
    {
        public AlphaLabelGenerator()
        {
            m_nextLabel = "A";
        }

        public string GenerateLabel()
        {
            string label = m_nextLabel;

            int labelLength = m_nextLabel.Length;
            char c = m_nextLabel[0];
            c++;

            if (c > 'Z')
            {
                c = 'A';
                labelLength++;
            }

            m_nextLabel = new string(c, labelLength);

            return label;
        }

        private string m_nextLabel = "A";
    }
}
