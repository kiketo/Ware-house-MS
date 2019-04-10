using Autofac;
using System;
using System.Linq;
using System.Reflection;
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
            var serviceAssembly = Assembly.Load("WHMS.Services");
            builder.RegisterAssemblyTypes(serviceAssembly).AsImplementedInterfaces();

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

            builder.RegisterType<InputProcessor>().As<IInputProcessor>().SingleInstance();
            builder.RegisterType<Report>().As<IReport>().SingleInstance();

        }
    }
}
