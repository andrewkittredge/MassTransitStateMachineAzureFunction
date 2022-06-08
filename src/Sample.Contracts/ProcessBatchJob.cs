namespace Sample.Contracts
{
    using System;

    public class ProcessBatchJob
    {
        public Guid BatchJobId { get; set; }

        public Guid OrderId { get; set; }
    }
}
