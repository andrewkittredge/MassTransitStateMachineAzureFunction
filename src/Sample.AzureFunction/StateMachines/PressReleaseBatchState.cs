namespace Sample.AzureFunction.StateMachines
{
    using System;
    using System.Collections.Generic;
    using MassTransit;

    internal class PressReleaseBatchState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }

        public int Total { get; set; }

        public string CurrentState { get; set; }

        public Stack<Guid> UnprocessedOrderIds { get; set; } = new Stack<Guid>();

        public Dictionary<Guid, Guid> ProcessingOrderIds { get; set; } = new Dictionary<Guid, Guid>(); // CorrelationId, OrderId
    }
}
