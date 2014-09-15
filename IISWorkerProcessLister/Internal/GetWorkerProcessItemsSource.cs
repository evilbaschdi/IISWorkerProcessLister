using Microsoft.Web.Administration;
using System;
using System.ComponentModel;

namespace IISWorkerProcessLister.Internal
{
    public class GetWorkerProcessItemsSource : IItemsSource
    {
        private readonly IApplicationPoolSitesAndApplications _applicationPoolSitesAndApplications;
        private readonly ServerManager _serverManager;
        private readonly IWorkerProcessItem _workerProcessItem;

        public GetWorkerProcessItemsSource(IApplicationPoolSitesAndApplications applicationPoolSitesAndApplications,
            IWorkerProcessItem workerProcessItem,
            ServerManager serverManager)
        {
            if (applicationPoolSitesAndApplications == null)
            {
                throw new ArgumentNullException("applicationPoolSitesAndApplications");
            }
            if (workerProcessItem == null)
            {
                throw new ArgumentNullException("workerProcessItem");
            }
            if (serverManager == null)
            {
                throw new ArgumentNullException("serverManager");
            }
            _applicationPoolSitesAndApplications = applicationPoolSitesAndApplications;
            _workerProcessItem = workerProcessItem;
            _serverManager = serverManager;
        }

        public BindingList<IWorkerProcessItem> Value
        {
            get
            {
                var itemsSource = new BindingList<IWorkerProcessItem>();
                itemsSource.Clear();
                //WorkerProcessInfo = string.Empty;
                //WorkerProcessShortInfo = string.Empty;

                foreach (var process in _serverManager.WorkerProcesses)
                {
                    var appPoolName = process.AppPoolName;
                    var applicationPoolSitesAndApplications =
                        _applicationPoolSitesAndApplications.Value(_serverManager.Sites, appPoolName);
                    var processId = process.ProcessId;
                    var state = process.State.ToString();

                    _workerProcessItem.ProcessId = processId;
                    _workerProcessItem.AppPoolName = appPoolName;
                    _workerProcessItem.Applications = applicationPoolSitesAndApplications;
                    _workerProcessItem.State = state;

                    var workerProcessInfo = string.Format("App Pool: {0} | Process ID: {1} | State: {2}{3}",
                        appPoolName,
                        processId, state, Environment.NewLine);
                    var workerPorocessShortInfo = string.Format("{0} | {1}{2}", appPoolName, processId,
                        Environment.NewLine);

                    //WorkerProcessInfo += workerProcessInfo;
                    //WorkerProcessShortInfo += workerPorocessShortInfo;

                    itemsSource.Add(_workerProcessItem);
                }

                return itemsSource;
            }
        }
    }
}