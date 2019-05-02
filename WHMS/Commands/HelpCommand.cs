using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WHMS.Commands.Contracts;

namespace WHMS.Commands
{
    public class HelpCommand : ICommand
    {
        private readonly IComponentContext components;

        public HelpCommand(IComponentContext components)
        {
            this.components = components;
        }

        public async Task<string> Execute(IReadOnlyList<string> args)
        {
            var commandNames = this.components.ComponentRegistry.Registrations
                .SelectMany(r => r.Services)
                .OfType<KeyedService>()
                .Select(ks => ks.ServiceKey).ToAsyncEnumerable();

            return "\r\nAvailable commands:\r\n============\r\n" + string.Join("\r\n", commandNames)+ "\r\n============\r\nPlease use ';' for separating command parameters!";
        }
    }
}
