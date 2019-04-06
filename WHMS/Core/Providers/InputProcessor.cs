using Autofac;
using Autofac.Core.Registration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WHMS.Commands.Contracts;
using WHMS.Core.Contracts;

namespace WHMS.Core.Providers
{
    public class InputProcessor : IInputProcessor
    {
        private readonly ILifetimeScope scope;

        public InputProcessor(ILifetimeScope scope)
        {
            this.scope = scope;
        }

        public string Process(string input)
        {
            using (var childScope = this.scope.BeginLifetimeScope())
            {
                var (name, args) = ParseInput(input);

                try
                {
                    var command = childScope.ResolveNamed<ICommand>(name);
                    return command.Execute(args);
                }
                catch (ComponentNotRegisteredException ex)
                {
                    return $"Tried to activate command {name} but got {ex.Message} \r\n  Please use 'help' for commands preview!";
                }
                catch (ArgumentException ex)
                {
                    return ex.Message;
                }
                catch (Exception ex) when (ex is DbUpdateException)
                {
                    while (ex.InnerException != null)
                        ex = ex.InnerException;

                    return $"Sql error occurred: {ex.Message}";
                }
            }
        }

        private (string name, IReadOnlyList<string> args) ParseInput(string input)
        {
            var parts = input.Split(";");

            return (parts[0], parts.Skip(1).ToList());
        }
    }
}
