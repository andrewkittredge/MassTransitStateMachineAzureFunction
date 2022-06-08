namespace Sample.AzureFunction
{
    using System.Threading;
    using System.Threading.Tasks;
    using Azure.Messaging.ServiceBus;
    using MassTransit;
    using Microsoft.Azure.WebJobs;
    using Sample.AzureFunction.Consumers;

    public class InitiatePressReleaseBatchFunction
    {
        private const string SubmitOrderQueueName = "getting-started";
        private readonly IMessageReceiver receiver;

        public InitiatePressReleaseBatchFunction(IMessageReceiver receiver)
        {
            this.receiver = receiver;
        }

        [FunctionName("InitiatePressReleaseBatch")]
        public Task SubmitOrderAsync([ServiceBusTrigger(SubmitOrderQueueName)] ServiceBusReceivedMessage message, CancellationToken cancellationToken)
        {
            return receiver.HandleConsumer<InitiatePressReleaseBatchConsumer>(SubmitOrderQueueName, message, cancellationToken);
        }
    }
}