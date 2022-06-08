namespace Sample.Contracts
{
    /// <summary>
    /// Corresponds to BatchReceived in the example.  This is what the statemachine consumes.
    /// </summary>
    public class StartPressReleaseBatch
    {
        public Guid BatchId { get; set; }

        public Guid[] OrderIds { get; set; }
    }
}