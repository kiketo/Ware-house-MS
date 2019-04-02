using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using WHMS.Commands.Contracts;
using WHMS.Core.Contracts;
using WHMSData.Context;

namespace WHMS.Core.Providers
{
    public class AutofacConfig
    {
        public IContainer Build()
        {
            var containerBuilder = new ContainerBuilder();

            this.RegisterConvention(containerBuilder);
            this.RegisterCoreComponents(containerBuilder);
            return containerBuilder.Build();
        }
        private void RegisterConvention(ContainerBuilder builder)
        {
            var coreAssembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(coreAssembly).AsImplementedInterfaces();

            var listCommands = coreAssembly.DefinedTypes.Where(typeOf => typeOf.ImplementedInterfaces
            .Contains(typeof(ICommand))).ToList();

            foreach (var command in listCommands)
            {
                builder.RegisterType(command.AsType())
                    .Named<ICommand>(command.Name.Replace("Command", "")
                    .ToLower());
            }
        }

        private void RegisterCoreComponents(ContainerBuilder builder)
        {
            builder.RegisterType<Engine>().As<IEngine>().SingleInstance();
            builder.RegisterType<WHMSContext>().AsSelf().InstancePerLifetimeScope();

            // builder.RegisterType<TravellerFactory>().As<ITravellerFactory>().SingleInstance();
            builder.RegisterType<CommandParser>().As<ICommandParser>().SingleInstance();
            builder.RegisterType<Report>().As<IReport>().SingleInstance();

        }
    }
}
