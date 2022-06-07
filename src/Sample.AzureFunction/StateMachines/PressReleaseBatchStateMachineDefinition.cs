namespace Sample.AzureFunction.StateMachines
{
    using System.Diagnostics;
    using MassTransit;
    using Sample.Contracts;

    internal class PressReleaseBatchStateMachineDefinition :
    SagaDefinition<PressReleaseBatchState>
    {
        public PressReleaseBatchStateMachineDefinition()
        {
            ConcurrentMessageLimit = 8;
        }

        protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator, ISagaConfigurator<PressReleaseBatchState> sagaConfigurator)
        {
            Debug.WriteLine("Configuring Saga");
            sagaConfigurator.UseMessageRetry(r => r.Immediate(5));
            sagaConfigurator.UseInMemoryOutbox();

            var partition = endpointConfigurator.CreatePartitioner(8);

            sagaConfigurator.Message<IStartPressReleaseBatch>(x => x.UsePartitioner(partition, m => m.Message.BatchId));
        }
    }
}
