using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using MassTransit;
using Microsoft.Azure.WebJobs;
using Sample.AzureFunction.Consumers;

namespace Sample.AzureFunction
{
    public class InitiatePressReleaseBatchFunction
    {
        const string SubmitOrderQueueName = "getting-started";
        readonly IMessageReceiver _receiver;

        public InitiatePressReleaseBatchFunction(IMessageReceiver receiver)
        {
            _receiver = receiver;
        }

        [FunctionName("InitiatePressReleaseBatch")]  
        public Task SubmitOrderAsync([ServiceBusTrigger(SubmitOrderQueueName)] ServiceBusReceivedMessage message, CancellationToken cancellationToken)
        {
            return _receiver.HandleConsumer<InitiatePressReleaseBatch>(SubmitOrderQueueName, message, cancellationToken);
        }
    }
}