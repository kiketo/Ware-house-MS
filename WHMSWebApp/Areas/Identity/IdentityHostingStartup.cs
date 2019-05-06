using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(WHMSWebApp2.Areas.Identity.IdentityHostingStartup))]
namespace WHMSWebApp2.Areas.Identity
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