using System;
using System.Threading.Tasks;
using MassTransit;
using Sample.Contracts;

namespace Sample.AzureFunction.Consumers
{
    public class InitiatePressReleaseBatch :
        IConsumer<StartPressReleaseBatchFromSender>
    {
        public Task Consume(ConsumeContext<StartPressReleaseBatchFromSender> context)
        {
            LogContext.Debug?.Log("Processing Order: {OrderNumber}", context.Message.OrderNumber);
            context.Publish<StartPressReleaseBatch>(new { });

            return context.RespondAsync<StartPressReleaseBatchFromSender>(context.Message);
        }
    }
}