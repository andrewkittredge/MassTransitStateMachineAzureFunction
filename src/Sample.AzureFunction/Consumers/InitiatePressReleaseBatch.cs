namespace Sample.AzureFunction.Consumers
{
    using System;
    using System.Threading.Tasks;
    using MassTransit;
    using Sample.Contracts;

    public class InitiatePressReleaseBatch :
        IConsumer<StartPressReleaseBatchFromSender>
    {
        public async Task Consume(ConsumeContext<StartPressReleaseBatchFromSender> context)
        {
            LogContext.Debug?.Log("Processing Order: {OrderNumber}", context.Message.OrderNumber);
            await context.Publish<StartPressReleaseBatch>(new { BatchId = NewId.NextGuid(), OrderIds = new Guid[] { NewId.NextGuid(), NewId.NextGuid() } });

            await context.RespondAsync<StartPressReleaseBatchFromSender>(context.Message);
        }
    }
}