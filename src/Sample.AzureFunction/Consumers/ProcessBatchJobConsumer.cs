namespace Sample.AzureFunction.Consumers
{
    using System.Threading.Tasks;
    using MassTransit;
    using Microsoft.Extensions.Logging;
    using Sample.Contracts;

    public class ProcessBatchJobConsumer :
        IConsumer<ProcessBatchJob>
    {
        readonly ILogger<ProcessBatchJobConsumer> _log;

        public ProcessBatchJobConsumer(ILoggerFactory loggerFactory)
        {
            _log = loggerFactory.CreateLogger<ProcessBatchJobConsumer>();
        }

        public async Task Consume(ConsumeContext<ProcessBatchJob> context)
        {
            using (_log.BeginScope("ProcessBatchJob {BatchJobId}, {OrderId}", context.Message.BatchJobId, context.Message.OrderId))
            {
                await context.Send(new BatchJobDone { BatchJobId = context.Message.BatchJobId });
            }
        }
    }
}
