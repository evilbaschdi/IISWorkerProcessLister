using System;
using Microsoft.Web.Administration;

namespace IISWorkerProcessLister.Internal
{
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
            if(workerProcessDataGridItem == null)
            {
                throw new ArgumentNullException(nameof(workerProcessDataGridItem));
            }
            if(serverManager == null)
            {
                throw new ArgumentNullException(nameof(serverManager));
            }
            _workerProcessDataGridItem = workerProcessDataGridItem;
            _serverManager = serverManager;
        }

        /// <summary>
        ///     Run.
        /// </summary>
        public void Run()
        {
            _serverManager.ApplicationPools[_workerProcessDataGridItem.Item.AppPoolName].Stop();
        }
    }
}