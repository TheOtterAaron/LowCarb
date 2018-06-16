using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using LowCarb;

namespace FileBaker
{
    public class ChipContractsFileWriter
    {
        static public void WriteFile(string destination, IChip chip)
        {
            File.WriteAllText(destination, GenerateFileContents(chip));
        }

        static private string GenerateFileContents(IChip chip)
        {
            StringBuilder fileContents = new StringBuilder();
            fileContents.AppendLine(string.Format("// {0}", chip.GetType().FullName));
            fileContents.AppendLine("// ");
            
            StringBuilder headerLine = new StringBuilder();
            headerLine.Append("// ");

            List<string> inputLabels = GetInputLabels(chip);
            foreach (string label in inputLabels)
            {
                headerLine.Append(String.Format("{0}  ", label));
            }
            headerLine.Append("|");

            foreach (string label in GetOutputLabels(chip))
            {
                headerLine.Append(String.Format("  {0}", label));
            }
            fileContents.AppendLine(headerLine.ToString());

            foreach (string input in GetInputCombinations(chip))
            {
                StringBuilder contractLine = new StringBuilder();
                contractLine.Append("   ");

                for (int i = 0; i < inputLabels.Count; i++)
                {
                    contractLine.Append(input[i]);
                    contractLine.Append(' ', inputLabels[i].Length + 1);
                }
                contractLine.Append("|  ");

                fileContents.AppendLine(contractLine.ToString());
            }

            return fileContents.ToString();
        }

        static private List<string> GetInputLabels(IChip chip)
        {
            List<string> inputLabels = new List<string>();
            foreach (Pin p in chip.GetInputPins())
            {
                inputLabels.Add(p.Label);
            }

            return inputLabels;
        }

        static private List<string> GetOutputLabels(IChip chip)
        {
            List<string> outputLabels = new List<string>();
            foreach (Pin p in chip.GetOutputPins())
            {
                outputLabels.Add(p.Label);
            }

            return outputLabels;
        }

        static private List<string> GetInputCombinations(IChip chip)
        {
            int inputPinCount = chip.GetInputPins().Count;

            List<string> inputCombinations = new List<string>();
            for (int i = 0; i < Math.Pow(2, inputPinCount); i++)
            {
                inputCombinations.Add(Convert.ToString(i, 2).PadLeft(inputPinCount, '0'));
            }

            return inputCombinations;
        }
    }
}
