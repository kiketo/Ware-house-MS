using System;
using System.Collections.Generic;
using System.Text;
using WHMS.Core.Contracts;

namespace WHMS.Core
{
    public class Engine : IEngine
    {
        private const string TerminationCommand = "Exit";
        private const string NullProvidersExceptionMessage = "cannot be null.";


        private IInputProcessor inputProcessor;
        private IReader reader;
        private IWriter writer;
        private IReport report;

        public Engine(
            IInputProcessor inputProcessor,
            IReader reader,
            IWriter writer,
            IReport report)
        {

            this.inputProcessor = inputProcessor;
            this.reader = reader;
            this.writer = writer;
            this.report = report;
        }

        public void Start()
        {
            while (true)
            {
                try
                {
                    var commandAsString = reader.ReadLine();

                    if (commandAsString.ToLower() == TerminationCommand.ToLower())
                    {
                        writer.Write(this.report.ForPrint());
                        break;
                    }

                    this.ProcessCommand(commandAsString);
                }
                catch (Exception ex)
                {
                    this.report.AppendLine(ex.Message);
                }
            }
        }

        private void ProcessCommand(string commandAsString)
        {
            if (string.IsNullOrWhiteSpace(commandAsString))
            {
                throw new ArgumentNullException("Command cannot be null or empty.");
            }

            var output = inputProcessor.Process(commandAsString);
            this.report.AppendLine(output);
        }
    }
}
