using System;
using System.IO;
using System.Text;

namespace FileBaker
{
    internal class ContractsCommand : ICommand
    {
        public int Execute(Invocation invocation)
        {
            if (invocation.Arguments.Count == 0)
            {
                Console.WriteLine("bake: 'contracts' command requires a destination argument.");
                return 102;
            }

            if (!invocation.OptionsContainsAlias("-s", "--source"))
            {
                Console.WriteLine("bake: 'contracts' command requires the '--source' option.");
                return 103;
            }

            if (!invocation.OptionsContainsAlias("-c", "--chip-class"))
            {
                Console.WriteLine("bake: 'contracts' command requires the '--chip-class' option.");
                return 104;
            }
            
            string destination = invocation.Arguments[0];
            string source = invocation.OptionsAtAlias("-s", "--source");
            string chipClass = invocation.OptionsAtAlias("-c", "--chip-class");            
            bool overwrite = invocation.OptionsContainsAlias("-f", "--force-overwrite");
            bool interactive = invocation.OptionsContainsAlias("-i", "--interactive");
            
            if (File.Exists(destination))
            {
                if (!interactive)
                {
                    Console.WriteLine(String.Format(
                        "bake: The destination file {0} already exists.  Use the '--force-overwrite' option to replace it.",
                        destination));
                    return 0;
                }
                else if (!PromptForOverwrite(destination))
                {
                    Console.WriteLine("Bake canceled.");
                    return 0;
                }
            }

            try
            {
                ChipContractsFileWriter.WriteFile(
                    destination,
                    ChipSourceReader.ReadSource(source, chipClass));
                Console.WriteLine(String.Format("Baked a chip contracts file at '{0}'", destination));
            }
            catch (Exception e)
            {
                Console.WriteLine("bake: " + e.Message);
                return 101;
            }
            
            return 0;
        }

        public string GetHelpMessage()
        {
            StringBuilder help = new StringBuilder();
            help.AppendLine("bake contracts [-i | --interacticve] [-f | --force-overwrite] -s | --source=<filepath>");
            help.AppendLine("                -c | --chip-class=<classpath> <destination>");
            help.AppendLine();
            help.AppendLine("     Creates a template contracts file for the chip in the specified source file with");
            help.AppendLine("     the specified classpath at a given destination filepath.  Chip classpath must include");
            help.AppendLine("     qualifying namespaces before the class name.");
            return help.ToString();
        }

        private bool PromptForOverwrite(string destination)
        {
            bool forceOverwrite = false;
            Console.WriteLine(
                String.Format("The file {0} already exists. Overwrite it? (Y/N)", destination));

            bool reading = true;
            do
            {
                switch (Console.ReadKey(true).KeyChar.ToString().ToUpper()[0])
                {
                    case 'Y':
                        forceOverwrite = true;
                        break;
                    case 'N':
                        break;
                    default:
                        continue;
                }
                reading = false;
            } while (reading);

            return forceOverwrite;
        }
    }
}
