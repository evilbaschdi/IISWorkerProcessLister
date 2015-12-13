﻿using System;
using System.Diagnostics;

namespace IISWorkerProcessLister.Internal
{
    /// <summary>
    /// </summary>
    public class CloseWorkerProcessByProcessId : IContextMenuEntry
    {
        private readonly IWorkerProcessDataGridItem _workerProcessDataGridItem;

        /// <summary>
        ///     Const5ructor of the class.
        /// </summary>
        /// <param name="workerProcessDataGridItem"></param>
        public CloseWorkerProcessByProcessId(IWorkerProcessDataGridItem workerProcessDataGridItem)
        {
            if(workerProcessDataGridItem == null)
            {
                throw new ArgumentNullException(nameof(workerProcessDataGridItem));
            }
            _workerProcessDataGridItem = workerProcessDataGridItem;
        }

        /// <summary>
        ///     Run.
        /// </summary>
        public void Run()
        {
            var process = Process.GetProcessById(_workerProcessDataGridItem.Item.ProcessId);

            process.CloseMainWindow();
            process.WaitForExit(10000);

            if(!process.HasExited)
            {
                process.Kill();
            }
        }
    }
}