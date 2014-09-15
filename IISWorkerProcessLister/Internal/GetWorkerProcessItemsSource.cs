using Microsoft.Web.Administration;
using System;
using System.ComponentModel;

namespace IISWorkerProcessLister.Internal
{
    public class GetWorkerProcessItemsSource : IItemsSource
    {
        private readonly IApplicationPoolSitesAndApplications _applicationPoolSitesAndApplications;
        private readonly IExtendedInformation _extendedInformation;
        private readonly ServerManager _serverManager;
        private readonly IShortInformation _shortInformation;
        private readonly IWorkerProcessItem _workerProcessItem;

        public GetWorkerProcessItemsSource(IApplicationPoolSitesAndApplications applicationPoolSitesAndApplications,
            IWorkerProcessItem workerProcessItem, ServerManager serverManager, IExtendedInformation extendedInformation,
            IShortInformation shortInformation)
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
            if (extendedInformation == null)
            {
                throw new ArgumentNullException("extendedInformation");
            }
            if (shortInformation == null)
            {
                throw new ArgumentNullException("shortInformation");
            }
            _applicationPoolSitesAndApplications = applicationPoolSitesAndApplications;
            _workerProcessItem = workerProcessItem;
            _serverManager = serverManager;
            _extendedInformation = extendedInformation;
            _shortInformation = shortInformation;
        }

        public BindingList<IWorkerProcessItem> Value
        {
            get
            {
                var itemsSource = new BindingList<IWorkerProcessItem>();
                itemsSource.Clear();
                _extendedInformation.Value = string.Empty;
                _shortInformation.Value = string.Empty;

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

                    _extendedInformation.Value += string.Format("App Pool: {0} | Process ID: {1} | State: {2}{3}",
                        appPoolName,
                        processId, state, Environment.NewLine);
                    _shortInformation.Value += string.Format("{0} | {1}{2}", appPoolName, processId,
                        Environment.NewLine);

                    itemsSource.Add(_workerProcessItem);
                }

                return itemsSource;
            }
        }
    }
}