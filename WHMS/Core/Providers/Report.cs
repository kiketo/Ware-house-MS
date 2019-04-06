using System;
using System.Collections.Generic;
using System.Text;
using WHMS.Core.Contracts;

namespace WHMS.Core.Providers
{
    public class Report : IReport
    {
        private StringBuilder stringBuilder;
        private DateTime start;
        public Report()
        {
            this.stringBuilder = new StringBuilder();
            //DateTime start = DateTime.Now;
            //this.AppendLine("The Engine is starting...");
        }
        public string ForPrint()
        {
            //DateTime end = DateTime.Now;
            //TimeSpan time = end - start;
            //this.AppendLine($"The Engine worked for {time.Milliseconds} milliseconds.");
            return this.stringBuilder.ToString();
        }
        public void AppendLine(string message)
        {
            this.stringBuilder.AppendLine(message);
        }
    }
}
