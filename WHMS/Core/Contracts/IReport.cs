using System;
using System.Collections.Generic;
using System.Text;

namespace WHMS.Core.Contracts
{
    public interface IReport
    {
        string ForPrint();
        void AppendLine(string message);
    }
}
