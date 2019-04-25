using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(WHMSWebApp.Areas.Identity.IdentityHostingStartup))]
namespace WHMSWebApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}