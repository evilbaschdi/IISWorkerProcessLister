namespace IISWorkerProcessLister.Internal
{
    public class GetWorkerProcessItem : IWorkerProcessItem
    {
        public int ProcessId { get; set; }

        public string AppPoolName { get; set; }

        public string State { get; set; }

        public string Sites { get; set; }

        public string Applications { get; set; }
    }
}