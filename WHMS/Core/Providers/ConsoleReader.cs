using System;
using System.Collections.Generic;
using System.Text;
using WHMS.Core.Contracts;

namespace WHMS.Core.Providers
{
    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }
    }
}
