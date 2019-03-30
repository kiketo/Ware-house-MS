using Autofac;
using WHMS.Core.Contracts;
using WHMS.Core.Providers;

namespace WHMS
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var config = new AutofacConfig();
            var container = config.Build();

            var engine = container.Resolve<IEngine>();
            engine.Start();

        }
    }
}
