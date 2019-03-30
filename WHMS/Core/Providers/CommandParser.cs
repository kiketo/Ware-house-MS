using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using WHMS.Commands.Contracts;
using WHMS.Core.Contracts;

namespace WHMS.Core.Providers
{
    internal class CommandParser : ICommandParser
    {
        private IComponentContext componentContext;

        public CommandParser(IComponentContext componentContext)
        {
            this.componentContext = componentContext ?? throw new ArgumentNullException(nameof(componentContext));
        }

        protected ICommand Resolve(string commandName)
        {
            return componentContext.ResolveNamed<ICommand>(commandName);
        }

        public ICommand ParseCommand(string fullCommand)
        {
            var commandName = fullCommand.Split()[0];
            return Resolve(commandName);
        }

        public IList<string> ParseParameters(string fullCommand)
        {
            var commandParts = fullCommand.Split().Skip(1).ToList();
            if (commandParts.Count == 0)
            {
                return new List<string>();
            }
            return commandParts;
        }
    }
}
