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
            Event(
                () => StartPressReleaseBatch,
                x => x.CorrelateById(c => c.Message.BatchId));
            Initially(When(StartPressReleaseBatch).Then(Initialize).Then(context =>
            {
                Debug.WriteLine("Got batch");
            }));
            DuringAny(When(StartPressReleaseBatch).Then(Initialize));
        }

        public Event<StartPressReleaseBatch> StartPressReleaseBatch { get; set; }

        private static void Initialize(BehaviorContext<PressReleaseBatchState, StartPressReleaseBatch> context)
        {
            InitializeInstance(context.Saga, context.Message);
        }

        private static void InitializeInstance(PressReleaseBatchState instance, StartPressReleaseBatch data)
        {
            instance.Total = data.OrderIds.Length;
        }
    }
}
