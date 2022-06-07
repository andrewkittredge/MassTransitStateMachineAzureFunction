namespace Sample.AzureFunction.StateMachines
{
    using System.Diagnostics;
    using MassTransit;
    using Sample.Contracts;

    internal class PressReleaseBatchStateMachine : MassTransitStateMachine<PressReleaseBatchState>
    {
        public PressReleaseBatchStateMachine()
        {
            Debug.WriteLine("Building State machine");
            InstanceState(x => x.CurrentState);
            Event(() => StartPressReleaseBatch, x => Debug.WriteLine("In event"));
            Initially(When(StartPressReleaseBatch).Then(context =>
            {
                Debug.WriteLine("Got batch");
            }));
            DuringAny(When(StartPressReleaseBatch).Then(Initialize));
        }

        public Event<StartPressReleaseBatch> StartPressReleaseBatch { get; set; }

        static void Initialize(BehaviorContext<PressReleaseBatchState, StartPressReleaseBatch> context)
        {
            InitializeInstance(context.Saga, context.Message);
        }

        static void InitializeInstance(PressReleaseBatchState instance, StartPressReleaseBatch data)
        {

            instance.Total = data.OrderIds.Length;
        }
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
            Debug.WriteLine("Configuring Saga");
            sagaConfigurator.UseMessageRetry(r => r.Immediate(5));
            sagaConfigurator.UseInMemoryOutbox();

            var partition = endpointConfigurator.CreatePartitioner(8);

            sagaConfigurator.Message<StartPressReleaseBatch>(x => x.UsePartitioner(partition, m => m.Message.BatchId));
        }
    }
}
