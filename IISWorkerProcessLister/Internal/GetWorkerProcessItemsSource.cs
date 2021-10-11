using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
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
            _applicationPoolSitesAndApplications = applicationPoolSitesAndApplications ?? throw new ArgumentNullException(nameof(applicationPoolSitesAndApplications));
            _workerProcessItem = workerProcessItem ?? throw new ArgumentNullException(nameof(workerProcessItem));
            _serverManager = serverManager ?? throw new ArgumentNullException(nameof(serverManager));
            _extendedInformation = extendedInformation ?? throw new ArgumentNullException(nameof(extendedInformation));
            _shortInformation = shortInformation ?? throw new ArgumentNullException(nameof(shortInformation));
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

                var extendedInformation = new StringBuilder();
                var shortInformation = new StringBuilder();

                try
                {
                    if (_serverManager.WorkerProcesses.Any())
                    {
                        foreach (var process in _serverManager.WorkerProcesses)
                        {
                            var appPoolName = process.AppPoolName;
                            var applicationPoolSitesAndApplications = _applicationPoolSitesAndApplications.Value(_serverManager.Sites, appPoolName);
                            var processId = process.ProcessId;
                            var state = process.State.ToString();

                            _workerProcessItem.ProcessId = processId;
                            _workerProcessItem.AppPoolName = appPoolName;
                            _workerProcessItem.Applications = applicationPoolSitesAndApplications;
                            _workerProcessItem.State = state;

                            extendedInformation.Append($"App Pool: {appPoolName} | Process ID: {processId} | State: {state}{Environment.NewLine}");
                            shortInformation.Append($"{appPoolName} | {processId}{Environment.NewLine}");

                            itemsSource.Add(_workerProcessItem);
                        }

                        _extendedInformation.Value = extendedInformation.ToString();
                        _shortInformation.Value = shortInformation.ToString();
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }

                return itemsSource;
            }
        }
    }
}