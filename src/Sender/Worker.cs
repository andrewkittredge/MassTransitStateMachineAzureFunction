namespace Sender
{
    using MassTransit;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Sample.AzureFunction;

    public class Worker : BackgroundService
    {
        private readonly IRequestClient<IStartPressReleaseBatchFromSender> client;
        private readonly ILogger<Worker> logger;
        private readonly IHost host;

        public Worker(IRequestClient<IStartPressReleaseBatchFromSender> client, ILogger<Worker> logger, IHost host)
        {
            this.client = client;
            this.logger = logger;
            this.host = host;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Sending batch");
            await client.GetResponse<IStartPressReleaseBatchFromSender>(new { OrderId = 123, OrderNumber = 456 });
            host.StopAsync();
        }
    }
}
