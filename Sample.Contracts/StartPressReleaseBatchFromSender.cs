using System;

namespace Sample.AzureFunction
{
    public interface StartPressReleaseBatchFromSender
    {
        Guid OrderId { get; }
        string OrderNumber { get; }
    }
}