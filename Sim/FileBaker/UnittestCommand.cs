using System;
using System.IO;
using System.Text;

namespace FileBaker
{
    internal class UnittestCommand : ICommand
    {
        public int Execute(Invocation invocation)
        {
            if (invocation.Arguments.Count == 0)
            {
                Console.WriteLine("bake: 'unittest' command requires a destination argument.");
                return 202;
            }

            if (!invocation.OptionsContainsAlias("-c", "--chip-name"))
            {
                Console.WriteLine("bake: 'unittest' command requires the '--chip-name' option.");
                return 203;
            }

            string destination = invocation.Arguments[0];
            string chipName = invocation.OptionsAtAlias("-c", "--chip-name");
            bool overwrite = invocation.OptionsContainsAlias("-f", "--force-overwrite");
            string chipNamespace = "LowCarb.BuiltIns";
            string contractsDirectory = "BuiltIns\\Contracts\\";

            if (invocation.OptionsContainsAlias("-n", "--namespace"))
            {
                chipNamespace = invocation.OptionsAtAlias("-n", "--namespace");
            }

            if (invocation.OptionsContainsAlias("-d", "--contracts-directory"))
            {
                contractsDirectory = invocation.OptionsAtAlias("-d", "--contracts-directory");
            }

            if (File.Exists(destination) && !overwrite)
            {
                Console.WriteLine(String.Format(
                        "bake: The destination file {0} already exists.  Use the '--force-overwrite' option to replace it.",
                        destination));
            }
            else
            {
                try
                {
                    File.WriteAllText(destination, WriteUnitTestFile(chipNamespace, chipName, contractsDirectory));
                    Console.WriteLine(String.Format("Baked a unit test source file at '{0}'", destination));
                }
                catch (Exception e)
                {
                    Console.WriteLine("bake: " + e.Message);
                    return 201;
                }
            }

            return 0;
        }

        public string GetHelpMessage()
        {
            StringBuilder help = new StringBuilder();
            help.AppendLine("bake unittest [-f | --force-overwrite] [-d | --contracts-directory=<directory>]");
            help.AppendLine("              [-n | --namespace=<string>] -c | --chip-name=<string> <destination>");
            help.AppendLine();
            help.AppendLine("     Creates a unit test source file for the chip with the specified name in the");
            help.AppendLine("     specified namespace at a given destination filepath.  The namespace must be");
            help.AppendLine("     fully qualified.  If a contracts directory is specified it should be relative");
            help.AppendLine("     to the containing test project's source directory, include a trailing slash");
            help.AppendLine("     and escape all special characters.");
            return help.ToString();
        }

        private string WriteUnitTestFile(string chipNamespace, string chipName, string contractsDirectory)
        {
            string rootNamespace = chipNamespace.Substring(0, chipNamespace.IndexOf('.'));
            string childNamespace = chipNamespace.Substring(rootNamespace.Length + 1);

            return
@"using Microsoft.VisualStudio.TestTools.UnitTesting;
using " + chipNamespace + @";
using LowCarb.Validation;

namespace " + rootNamespace + @"Tests." + childNamespace + @"
{
    [TestClass]
    public class " + chipName + @"Tests
    {
        [TestMethod]
        public void Test" + chipName + @"Contracts()
        {
            ChipValidator<" + chipName + @"> chipValidator = new ChipValidator<" + chipName + @">();
            Assert.IsTrue(chipValidator.AddContracts(ChipContractsFileReader.ReadContracts(""" + contractsDirectory + chipName + @"ContractsFile.txt"")));
            Assert.IsTrue(chipValidator.Test());
        }
    }
}
";
        }
    }
}
