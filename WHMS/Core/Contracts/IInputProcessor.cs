using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WHMS.Core.Contracts
{
    public interface IInputProcessor
    {
        Task<string> Process(string arguments);
    }
}
