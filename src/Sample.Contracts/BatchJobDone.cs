namespace Sample.Contracts
{
    using System;


    /// <summary>
    /// A single batch job has compeleted.
    /// </summary>
    public class BatchJobDone
    {
        public Guid BatchJobId { get; set; }

        internal Guid BatchId { get; }

        internal DateTime Timestamp { get; }
    }
}
