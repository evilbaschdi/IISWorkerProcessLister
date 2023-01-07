using Microsoft.Web.Administration;

namespace IISWorkerProcessLister.Internal;

/// <summary>
/// </summary>
public class StopApplicationPool : IContextMenuEntry
{
    private readonly ServerManager _serverManager;
    private readonly IWorkerProcessDataGridItem _workerProcessDataGridItem;

    /// <summary>
    ///     Constructor of the class.
    /// </summary>
    /// <param name="workerProcessDataGridItem"></param>
    /// <param name="serverManager"></param>
    public StopApplicationPool(IWorkerProcessDataGridItem workerProcessDataGridItem, ServerManager serverManager)
    {
        _workerProcessDataGridItem = workerProcessDataGridItem ?? throw new ArgumentNullException(nameof(workerProcessDataGridItem));
        _serverManager = serverManager ?? throw new ArgumentNullException(nameof(serverManager));
    }

    /// <summary>
    ///     Run.
    /// </summary>
    public void Run()
    {
        _serverManager.ApplicationPools[_workerProcessDataGridItem.Value.AppPoolName].Stop();
    }
}