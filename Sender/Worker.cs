using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sample.AzureFunction;

namespace Sender
{
    public class Worker : BackgroundService
    {
        readonly IRequestClient<StartPressReleaseBatchFromSender> _client;
        readonly ILogger<Worker> _logger;
        private readonly IHost _host;

        public Worker(IRequestClient<StartPressReleaseBatchFromSender> client, ILogger<Worker> logger, IHost host)
        {
            _client = client;
            _logger = logger;
            _host = host;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Sending batch");
            await _client.GetResponse<StartPressReleaseBatchFromSender>(new { OrderId = 123, OrderNumber = 456 });
            _host.StopAsync();  

        }
    }
}
