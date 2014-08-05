namespace IISWorkerProcessLister.Internal
{
    public class WorkerProcessItem
    {
        public int ProcessId { get; set; }
        public string AppPoolName { get; set; }
        public string State { get; set; }
        public string Sites { get; set; }
        //public string Uri { get; set; }
        //public string Port { get; set; }
        public string Applications { get; set; }
    }
}