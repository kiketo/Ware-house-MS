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

        private IDataBase dataBase;
        private ICommandParser commandParser;
        private IReader reader;
        private IWriter writer;
        private IReport report;

        public Engine(IDataBase dataBase,
            ICommandParser commandParser,
            IReader reader,
            IWriter writer,
            IReport report)
        {
            this.dataBase = dataBase;
            this.commandParser = commandParser;
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

            var command = commandParser.ParseCommand(commandAsString);
            var parameters = commandParser.ParseParameters(commandAsString);

            var executionResult = command.Execute(parameters);
            this.report.AppendLine(executionResult);
        }
    }
}
