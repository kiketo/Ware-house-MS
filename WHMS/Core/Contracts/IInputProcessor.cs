using System;
using System.Collections.Generic;
using System.Text;

namespace WHMS.Core.Contracts
{
    public interface IInputProcessor
    {
        string Process(string arguments);
    }
}
