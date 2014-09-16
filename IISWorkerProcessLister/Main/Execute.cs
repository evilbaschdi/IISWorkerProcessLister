using IISWorkerProcessLister.Internal;
using Microsoft.Web.Administration;
using System;
using System.Windows.Forms;

namespace IISWorkerProcessLister.Main
{
    public class Execute : IMain
    {
        private readonly MainWindow _mainWindow;

        public Execute(MainWindow mainWindow)
        {
            if (mainWindow == null)
            {
                throw new ArgumentNullException("mainWindow");
            }
            _mainWindow = mainWindow;
        }

        public void Run()
        {
            var applicationPoolApplications = new GetApplicationPoolApplications();
            var serverManager = new ServerManager();
            var workerProcessItem = new GetWorkerProcessItem();
            var extendedInformation = new ExtendedInformation();
            var shortInformation = new ShortInformation();
            var notifyIcon = new NotifyIcon();
            var applicationPoolSitesAndApplications =
                new ReturnApplicationPoolSitesAndApplications(applicationPoolApplications);
            var itemsSource = new GetWorkerProcessItemsSource(applicationPoolSitesAndApplications, workerProcessItem,
                serverManager, extendedInformation, shortInformation);
            _mainWindow.WorkerProcessesDataGrid.ItemsSource = itemsSource.Value;
            var workerProcessInformation = new WorkerProcessInformation(notifyIcon,
                extendedInformation, shortInformation);
            workerProcessInformation.Set();
        }
    }
}