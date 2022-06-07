using MassTransit;
using System;

namespace Sample.AzureFunction.StateMachines
{
    internal class PressReleaseBatchState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Total;

        public string CurrentState { get; set; }
    }
}
