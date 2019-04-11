using System;
using System.Collections.Generic;
using System.Text;

namespace WHMS.Services.Contracts
{
    public interface IJSONExplorer<T>
    {
        List<T> Read(string jPath, string jName);

        void Write(List<T> list, string jPath, string jName);
    }
}
