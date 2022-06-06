using MassTransit;
using Sample.Contracts;
using System.Diagnostics;

namespace Sample.AzureFunction.StateMachines
{
    internal class PressReleaseBatchStateMachine : MassTransitStateMachine<PressReleaseBatchState>
    {
        public PressReleaseBatchStateMachine()
        {
            Initially(When(StartPressReleaseBatch).Then(context =>
            {
                Debug.WriteLine("Got batch");
            }));
        }

        public Event<Contracts.StartPressReleaseBatch> StartPressReleaseBatch { get; set; }


    }

    internal class PressReleaseBatchStateMachineDefinition :
    SagaDefinition<PressReleaseBatchState>
    {
        public PressReleaseBatchStateMachineDefinition()
        {
            ConcurrentMessageLimit = 8;
        }

        protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator, ISagaConfigurator<PressReleaseBatchState> sagaConfigurator)
        {
            sagaConfigurator.UseMessageRetry(r => r.Immediate(5));
            sagaConfigurator.UseInMemoryOutbox();

            var partition = endpointConfigurator.CreatePartitioner(8);

            sagaConfigurator.Message<Contracts.StartPressReleaseBatch>(x => x.UsePartitioner(partition, m => m.Message.BatchId));
        }
    }
}
