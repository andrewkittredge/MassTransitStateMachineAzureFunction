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
            Event(() => BatchJobDone, x => x.CorrelateById(c => c.Message.BatchJobId));
            Initially(When(StartPressReleaseBatch).Then(Initialize).ThenAsync(async context =>
            {
                foreach (var item in context.Saga.UnprocessedOrderIds)
                {
                    await context.Publish(new ProcessBatchJob { BatchJobId = NewId.NextGuid(), OrderId = item });
                }
            }));
            DuringAny(When(BatchJobDone).If(
                context => context.Saga.UnprocessedOrderIds.Count == 0 && context.Saga.ProcessingOrderIds.Count == 0,
                binder => binder.Then(x => { Debug.WriteLine("Done with batch"); })));
        }

        /// <summary>
        /// Gets or sets all the batch jobs have finished.
        /// </summary>
        public State AllBatchJobsFinished { get; set; }

        public Event<StartPressReleaseBatch> StartPressReleaseBatch { get; set; }

        public Event<BatchJobDone> BatchJobDone { get; set; }


        private static void Initialize(BehaviorContext<PressReleaseBatchState, StartPressReleaseBatch> context)
        {
            InitializeInstance(context.Saga, context.Message);
        }

        private static void InitializeInstance(PressReleaseBatchState instance, StartPressReleaseBatch data)
        {
            instance.Total = data.OrderIds.Length;
            foreach (var item in data.OrderIds)
            {
                instance.UnprocessedOrderIds.Push(item);
            }
        }
    }
}
