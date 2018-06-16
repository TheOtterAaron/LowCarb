using System.Collections.Generic;

namespace FileBaker
{
    internal interface ICommand
    {
        int Execute(Invocation invocation);
        string GetHelpMessage();
    }
}
