using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sample.AzureFunction;

namespace Sender
{
    public class Worker: BackgroundService
    {
        readonly IRequestClient<SubmitOrder> _client;
        readonly ILogger<Worker> _logger;

        public Worker(IRequestClient<SubmitOrder> client, ILogger<Worker> logger)
        {
            _client = client;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _client.GetResponse<OrderAccepted>(new { OrderId = 123 });
            
        }
    }
}
