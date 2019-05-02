using System.Collections.Generic;
using System.Threading.Tasks;

namespace WHMS.Commands.Contracts
{
    public interface ICommand
    {
        Task<string> Execute(IReadOnlyList<string> parameters);
    }
}
