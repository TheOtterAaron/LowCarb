using System;
using System.Text;

namespace FileBaker
{
    internal class HelpCommand : ICommand
    {
        public int Execute(Invocation invocation)
        {
            string commandName = "help";

            if (invocation.Arguments.Count > 0)
            {
                commandName = invocation.Arguments[0];
            }

            Console.WriteLine(CommandFactory.BuildCommand(commandName).GetHelpMessage());

            return 0;
        }

        public string GetHelpMessage()
        {
            StringBuilder helpMessage = new StringBuilder();
            helpMessage.AppendLine("usage: bake <command> [<args>]");
            helpMessage.AppendLine("");
            helpMessage.AppendLine("available commands:");
            helpMessage.AppendLine("  contracts");
            helpMessage.AppendLine("  fillproject");
            helpMessage.AppendLine("  unittest");

            return helpMessage.ToString();
        }
    }
}
