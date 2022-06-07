using MassTransit;
using Sample.Contracts;
using System.Diagnostics;

namespace Sample.AzureFunction.StateMachines
{
    internal class PressReleaseBatchStateMachine : MassTransitStateMachine<PressReleaseBatchState>
    {
        public PressReleaseBatchStateMachine()
        {
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
            //instance.Action = data.Action;
            instance.Total = data.OrderIds.Length;
           // instance.UnprocessedOrderIds = new Stack<Guid>(data.OrderIds);
           // instance.ActiveThreshold = data.ActiveThreshold;
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
            sagaConfigurator.UseMessageRetry(r => r.Immediate(5));
            sagaConfigurator.UseInMemoryOutbox();

            var partition = endpointConfigurator.CreatePartitioner(8);

            sagaConfigurator.Message<StartPressReleaseBatch>(x => x.UsePartitioner(partition, m => m.Message.BatchId));
        }
    }
}
