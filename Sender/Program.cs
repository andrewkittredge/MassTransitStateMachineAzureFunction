using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sample.AzureFunction;
using Sample.AzureFunction.Consumers;

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
            var builder = Host.CreateDefaultBuilder(args);
            builder.ConfigureServices((_, services) =>
            {
                services.AddMassTransit(cfg =>
                {
                    cfg.AddRequestClient<SubmitOrder>(new Uri("queue:getting-started"));
                    cfg.UsingAzureServiceBus((context, cfg) =>
                    {
                        cfg.Host("Endpoint=sb://cbandrewtest.servicebus.windows.net/;SharedAccessKeyName=MassTransit;SharedAccessKey=fD4VhRMToqsTBpfdyjAH1nqC7xIkqspjHaJUP9cdELQ=");
                        cfg.ConfigureEndpoints(context);
                    });
                });
                services.AddHostedService<Worker>();
            });
            return builder;
        }
    }
}