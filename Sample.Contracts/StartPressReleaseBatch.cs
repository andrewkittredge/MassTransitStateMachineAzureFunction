namespace Sample.Contracts
{

    /// <summary>
    /// Coorespondes to BatchReceived in the example.
    /// </summary>
    public interface StartPressReleaseBatch
    {
        Guid BatchId { get; }
        Guid[] OrderIds { get; }
    }
}