using System.Collections.Generic;

namespace WHMS.Commands.Contracts
{
    public interface ICommand
    {
        string Execute(IReadOnlyList<string> parameters);
    }
}
