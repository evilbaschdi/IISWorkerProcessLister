using System;
using System.Diagnostics;

namespace IISWorkerProcessLister.Internal
{
    public class CloseWorkerProcessByProcessId : IWorkerProcess
    {
        private readonly IWorkerProcessDataGridItem _workerProcessDataGridItem;

        public CloseWorkerProcessByProcessId(IWorkerProcessDataGridItem workerProcessDataGridItem)
        {
            if (workerProcessDataGridItem == null)
            {
                throw new ArgumentNullException("workerProcessDataGridItem");
            }
            _workerProcessDataGridItem = workerProcessDataGridItem;
        }

        public void Run()
        {
            var process = Process.GetProcessById(_workerProcessDataGridItem.Item.ProcessId);

            process.CloseMainWindow();
            process.WaitForExit(10000);

            if (!process.HasExited)
            {
                process.Kill();
            }
        }
    }
}