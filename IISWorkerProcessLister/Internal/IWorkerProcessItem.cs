namespace IISWorkerProcessLister.Internal
{
    public interface IWorkerProcessItem
    {
        int ProcessId { get; set; }

        string AppPoolName { get; set; }

        string State { get; set; }

        string Sites { get; set; }

        //public string Uri { get; set; }
        //public string Port { get; set; }
        string Applications { get; set; }
    }
}