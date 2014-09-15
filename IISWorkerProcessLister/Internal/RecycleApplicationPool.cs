using Microsoft.Web.Administration;
using System;

namespace IISWorkerProcessLister.Internal
{
    public class RecycleApplicationPool : IWorkerProcess
    {
        private readonly ServerManager _serverManager;
        private readonly IWorkerProcessDataGridItem _workerProcessDataGridItem;

        public RecycleApplicationPool(IWorkerProcessDataGridItem workerProcessDataGridItem, ServerManager serverManager)
        {
            if (workerProcessDataGridItem == null)
            {
                throw new ArgumentNullException("workerProcessDataGridItem");
            }
            if (serverManager == null)
            {
                throw new ArgumentNullException("serverManager");
            }
            _workerProcessDataGridItem = workerProcessDataGridItem;
            _serverManager = serverManager;
        }

        public void Run()
        {
            _serverManager.ApplicationPools[_workerProcessDataGridItem.Item.AppPoolName].Recycle();
        }
    }
}