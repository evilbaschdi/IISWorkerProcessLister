namespace IISWorkerProcessLister.Internal
{
    /// <summary>
    /// </summary>
    public class GetWorkerProcessItem : IWorkerProcessItem
    {
        /// <summary>
        /// </summary>
        public int ProcessId { get; set; }

        /// <summary>
        /// </summary>
        public string AppPoolName { get; set; }

        /// <summary>
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// </summary>
        public string Sites { get; set; }

        /// <summary>
        /// </summary>
        public string Applications { get; set; }
    }
}