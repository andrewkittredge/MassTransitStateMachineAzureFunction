namespace Sample.AzureFunction.StateMachines
{
    using System;
    using MassTransit;

    internal class PressReleaseBatchState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }

        public int Total { get; set; }

        public string CurrentState { get; set; }
    }
}
