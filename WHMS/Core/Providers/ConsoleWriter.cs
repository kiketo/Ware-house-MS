using System;
using System.Collections.Generic;
using System.Text;
using WHMS.Core.Contracts;

namespace WHMS.Core.Providers
{
    public class ConsoleWriter : IWriter
    {
        public void Write(string text)
        {
            Console.Write(text);
        }
    }
}
