namespace Sample.Contracts
{
    /// <summary>
    /// Coorespondes to BatchReceived in the example.  This is what the statemachine is supposed to consume.
    /// </summary>
    public interface IStartPressReleaseBatch
    {
        Guid BatchId { get; }

        Guid[] OrderIds { get; }
    }
}