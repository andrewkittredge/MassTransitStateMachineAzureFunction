using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sample.AzureFunction;

namespace Sender
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args).Build();
            IConfiguration config = host.Services.GetRequiredService<IConfiguration>();
            string serviceBusConnectionString = config.GetValue<string>("AzureWebJobsServiceBus");
            var builder = Host.CreateDefaultBuilder(args);            
            builder.ConfigureServices((hostBuilder, services) =>
            {
                
                services.AddMassTransit(cfg =>
                {
                    cfg.AddRequestClient<StartPressReleaseBatchFromSender>(new Uri("queue:getting-started"));
                    cfg.UsingAzureServiceBus((context, cfg) =>
                    {
                        cfg.Host(serviceBusConnectionString);
                        cfg.ConfigureEndpoints(context);
                    });
                });
                services.AddHostedService<Worker>();
            });
            return builder;
        }
    }
}