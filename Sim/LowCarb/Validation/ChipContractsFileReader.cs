using System.IO;
using System.Collections.Generic;

namespace LowCarb.Validation
{
    public class ChipContractsFileReader
    {
        public static IList<ChipContract> ReadContracts(string path)
        {
            List<ChipContract> contracts = new List<ChipContract>();

            string[] contractStrings = File.ReadAllLines(path);
            foreach (string contractString in contractStrings)
            {
                if (contractString.Length < 2)
                {
                    continue;
                }

                if (contractString.Substring(0, 2) == "//")  // Comment
                {
                    continue;
                }

                string[] contractHalves = contractString.Split('|');
                if (contractHalves.Length < 2)
                {
                    continue;
                }

                List<int> inputs = ExtractZeroesAndOnes(contractHalves[0]);
                List<int> outputs = ExtractZeroesAndOnes(contractHalves[1]);

                contracts.Add(new ChipContract(inputs.ToArray(), outputs.ToArray()));
            }

            return contracts.AsReadOnly();
        }

        private static List<int> ExtractZeroesAndOnes(string source)
        {
            List<int> digits = new List<int>();
            foreach (char c in source)
            {
                if (c == '0')
                {
                    digits.Add(0);
                }
                else if (c == '1')
                {
                    digits.Add(1);
                }
            }

            return digits;
        }
    }
}
