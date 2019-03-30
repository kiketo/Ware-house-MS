using System;
using System.Collections.Generic;
using System.Text;
using WHMS.Commands.Contracts;

namespace WHMS.Core.Contracts
{
    interface ICommandParser
    {
        ICommand ParseCommand(string fullCommand);
        IList<string> ParseParameters(string fullCommand);
    }
}
