using IISWorkerProcessLister.Internal;
using Microsoft.Web.Administration;

namespace IISWorkerProcessLister.Main;

/// <summary>
/// </summary>
public class Execute : IMain
{
    private readonly MainWindow _mainWindow;

    /// <summary>
    /// </summary>
    /// <param name="mainWindow"></param>
    public Execute(MainWindow mainWindow)
    {
        _mainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));
    }

    /// <summary>
    /// </summary>
    public void Run()
    {
        var applicationPoolApplications = new GetApplicationPoolApplications();
        var serverManager = new ServerManager();
        var workerProcessItem = new GetWorkerProcessItem();
        var extendedInformation = new ExtendedInformation();
        var shortInformation = new ShortInformation();

        var applicationPoolSitesAndApplications = new ReturnApplicationPoolSitesAndApplications(applicationPoolApplications);
        var itemsSource = new GetWorkerProcessItemsSource(applicationPoolSitesAndApplications, workerProcessItem, serverManager, extendedInformation, shortInformation);
        _mainWindow.WorkerProcessesDataGrid.SetCurrentValue(System.Windows.Controls.ItemsControl.ItemsSourceProperty, itemsSource.Value);
        var workerProcessInformation = new WorkerProcessInformation(_mainWindow, shortInformation);
        workerProcessInformation.Run();
    }
}