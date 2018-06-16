using System;
using System.Text;

namespace FileBaker
{
    internal class CommandFactory
    {
        static public ICommand BuildCommand(string commandName)
        {
            ICommand command;

            switch (commandName)
            {
                case "":
                case "help":
                    command = new HelpCommand();
                    break;
                    
                case "contracts":
                    command = new ContractsCommand();
                    break;

                case "fillproject":
                    command = new FillprojectCommand();
                    break;

                case "unittest":
                    command = new UnittestCommand();
                    break;

                default:
                    command = null;
                    break;
            }

            return command;
        }
    }
}
