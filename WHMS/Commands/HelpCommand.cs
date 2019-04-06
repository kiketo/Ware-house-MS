using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public string Execute(IReadOnlyList<string> args)
        {
            var commandNames = this.components.ComponentRegistry.Registrations
                .SelectMany(r => r.Services)
                .OfType<KeyedService>()
                .Select(ks => ks.ServiceKey);

            return "Available commands:\r\n" + string.Join("\r\n", commandNames);
        }
    }
}
