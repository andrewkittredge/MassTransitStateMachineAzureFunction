namespace Sample.AzureFunction
{
    using System;

    /// <summary>
    /// This is what the sender sends to the function to kick off the batch.
    /// </summary>
    public interface IStartPressReleaseBatchFromSender
    {
        Guid OrderId { get; }

        string OrderNumber { get; }
    }
}