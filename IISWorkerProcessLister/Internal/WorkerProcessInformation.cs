namespace IISWorkerProcessLister.Internal;

/// <summary>
/// </summary>
public class WorkerProcessInformation : IWorkerProcessInformation
{
    private readonly MainWindow _mainWindow;
    private readonly IShortInformation _shortInformation;

    /// <summary>
    /// </summary>
    /// <param name="mainWindow"></param>
    /// <param name="shortInformation"></param>
    public WorkerProcessInformation(MainWindow mainWindow, IShortInformation shortInformation)
    {
        _mainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));
        _shortInformation = shortInformation;
    }

    /// <summary>
    /// </summary>
    public void Run()
    {
        _mainWindow.TaskbarItemInfo.SetCurrentValue(System.Windows.Shell.TaskbarItemInfo.DescriptionProperty, _shortInformation.Value);
    }
}