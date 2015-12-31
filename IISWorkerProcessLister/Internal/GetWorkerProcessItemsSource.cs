using System;
using System.ComponentModel;
using Microsoft.Web.Administration;

namespace IISWorkerProcessLister.Internal
{
    /// <summary>
    ///     Class that adds workerprocess strings to binding list.
    /// </summary>
    public class GetWorkerProcessItemsSource : IItemsSource
    {
        private readonly IApplicationPoolSitesAndApplications _applicationPoolSitesAndApplications;
        private readonly IExtendedInformation _extendedInformation;
        private readonly ServerManager _serverManager;
        private readonly IShortInformation _shortInformation;
        private readonly IWorkerProcessItem _workerProcessItem;

        /// <summary>
        ///     Constructor of the class.
        /// </summary>
        /// <param name="applicationPoolSitesAndApplications"></param>
        /// <param name="workerProcessItem"></param>
        /// <param name="serverManager"></param>
        /// <param name="extendedInformation"></param>
        /// <param name="shortInformation"></param>
        public GetWorkerProcessItemsSource(IApplicationPoolSitesAndApplications applicationPoolSitesAndApplications,
            IWorkerProcessItem workerProcessItem, ServerManager serverManager, IExtendedInformation extendedInformation,
            IShortInformation shortInformation)
        {
            if(applicationPoolSitesAndApplications == null)
            {
                throw new ArgumentNullException(nameof(applicationPoolSitesAndApplications));
            }
            if(workerProcessItem == null)
            {
                throw new ArgumentNullException(nameof(workerProcessItem));
            }
            if(serverManager == null)
            {
                throw new ArgumentNullException(nameof(serverManager));
            }
            if(extendedInformation == null)
            {
                throw new ArgumentNullException(nameof(extendedInformation));
            }
            if(shortInformation == null)
            {
                throw new ArgumentNullException(nameof(shortInformation));
            }
            _applicationPoolSitesAndApplications = applicationPoolSitesAndApplications;
            _workerProcessItem = workerProcessItem;
            _serverManager = serverManager;
            _extendedInformation = extendedInformation;
            _shortInformation = shortInformation;
        }

        /// <summary>
        ///     BindingList with WorkerProcess strings.
        /// </summary>
        public BindingList<IWorkerProcessItem> Value
        {
            get
            {
                var itemsSource = new BindingList<IWorkerProcessItem>();
                itemsSource.Clear();
                _extendedInformation.Value = string.Empty;
                _shortInformation.Value = string.Empty;

                if(_serverManager.WorkerProcesses != null)
                {
                    foreach(var process in _serverManager.WorkerProcesses)
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

                        _extendedInformation.Value +=
                            $"App Pool: {appPoolName} | Process ID: {processId} | State: {state}{Environment.NewLine}";
                        _shortInformation.Value += $"{appPoolName} | {processId}{Environment.NewLine}";

                        itemsSource.Add(_workerProcessItem);
                    }
                }

                return itemsSource;
            }
        }
    }
}