using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace FileBaker
{
    class Program
    {
        static int Main(string[] args)
        {
            int exitCode = 0;

            try
            {
                Invocation invocation = new Invocation();
                ParseArgs(args, invocation);

                ICommand command = CommandFactory.BuildCommand(invocation.Command);
                if (command != null)
                {
                    exitCode = command.Execute(invocation);
                }
                else
                {
                    StringBuilder error = new StringBuilder();
                    error.AppendLine("bake: '{0}' is not a recognized bake command.");
                    error.AppendLine("      See 'bake help' for a list of available commands.");
                    Console.WriteLine(String.Format(error.ToString(), invocation.Command));

                    exitCode = 1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("fatal: {0}", e);

                exitCode = 5;
            }

            return exitCode;
        }

        private static void ParseArgs(string[] args, Invocation invocation)
        {
            foreach (string s in args)
            {
                if (s.StartsWith("-"))
                {
                    ParseOption(s, invocation);
                }
                else if (invocation.Command == "")
                {
                    invocation.Command = s;
                }
                else
                {
                    invocation.Arguments.Add(s);
                }
            }
        }

        private static void ParseOption(string option, Invocation invocation)
        {
            string optionName = option.Split('=')[0];
            string optionValue = "";

            if (option.Contains("="))
            {
                optionValue = option.Substring(option.IndexOf('=')).TrimStart('=');
            }

            invocation.Options.Add(optionName, optionValue);
        }
    }
}
