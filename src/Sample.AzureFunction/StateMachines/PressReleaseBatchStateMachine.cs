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

        public Event<IStartPressReleaseBatch> StartPressReleaseBatch { get; set; }

        private static void Initialize(BehaviorContext<PressReleaseBatchState, IStartPressReleaseBatch> context)
        {
            InitializeInstance(context.Saga, context.Message);
        }

        private static void InitializeInstance(PressReleaseBatchState instance, IStartPressReleaseBatch data)
        {
            instance.Total = data.OrderIds.Length;
        }
    }
}
