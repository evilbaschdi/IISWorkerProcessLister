namespace IISWorkerProcessLister.Internal;

/// <summary>
/// </summary>
public interface IWorkerProcessItem
{
    /// <summary>
    /// </summary>
    //public string Uri { get; set; }
    //public string Port { get; set; }
    string Applications { get; set; }

    /// <summary>
    /// </summary>
    string AppPoolName { get; set; }

    /// <summary>
    /// </summary>
    int ProcessId { get; set; }

    /// <summary>
    /// </summary>
    string Sites { get; set; }

    /// <summary>
    /// </summary>
    string State { get; set; }
}