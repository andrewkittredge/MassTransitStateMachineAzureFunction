﻿namespace Sample.AzureFunction.StateMachines
{
    using System;
    using MassTransit;

    internal class PressReleaseBatchState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Total { get; set; }

        public string CurrentState { get; set; }
    }
}
