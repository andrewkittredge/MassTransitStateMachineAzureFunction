using System;

namespace Sample.AzureFunction
{
    /// <summary>
    /// This is what the sender sends to the function to kick off the batch
    /// </summary>
    public interface StartPressReleaseBatchFromSender
    {
        Guid OrderId { get; }
        string OrderNumber { get; }
    }
}