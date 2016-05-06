using System;
using IISWorkerProcessLister.Internal;
using Microsoft.Web.Administration;

namespace IISWorkerProcessLister.Main
{
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
            if (mainWindow == null)
            {
                throw new ArgumentNullException(nameof(mainWindow));
            }

            _mainWindow = mainWindow;
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
            _mainWindow.WorkerProcessesDataGrid.ItemsSource = itemsSource.Value;
            var workerProcessInformation = new WorkerProcessInformation(_mainWindow, extendedInformation, shortInformation);
            workerProcessInformation.Set();
        }
    }
}