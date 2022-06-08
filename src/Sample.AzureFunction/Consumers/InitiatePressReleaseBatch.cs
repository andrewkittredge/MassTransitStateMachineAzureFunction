namespace Sample.AzureFunction.Consumers
{
    using System;
    using System.Threading.Tasks;
    using MassTransit;
    using Sample.Contracts;

    /// <summary>
    /// Starts the batch saga.
    /// </summary>
    public class InitiatePressReleaseBatchConsumer :
        IConsumer<IStartPressReleaseBatchFromSender>
    {
        public async Task Consume(ConsumeContext<IStartPressReleaseBatchFromSender> context)
        {
            LogContext.Debug?.Log("Processing Order: {OrderNumber}", context.Message.OrderNumber);
            await context.Publish(new StartPressReleaseBatch { BatchId = NewId.NextGuid(), OrderIds = new Guid[] { NewId.NextGuid(), NewId.NextGuid() } });

            await context.RespondAsync<IStartPressReleaseBatchFromSender>(context.Message);
        }
    }
}