using System;

namespace IISWorkerProcessLister.Internal
{
    /// <summary>
    /// </summary>
    public class WorkerProcessInformation : IWorkerProcessInformation
    {
        private readonly MainWindow _mainWindow;
        private readonly IExtendedInformation _extendedInformation;
        private readonly IShortInformation _shortInformation;

        /// <summary>
        /// </summary>
        /// <param name="mainWindow"></param>
        /// <param name="extendedInformation"></param>
        /// <param name="shortInformation"></param>
        public WorkerProcessInformation(MainWindow mainWindow, IExtendedInformation extendedInformation, IShortInformation shortInformation)
        {
            _mainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));
            _extendedInformation = extendedInformation;
            _shortInformation = shortInformation;
        }

        /// <summary>
        /// </summary>
        public void Set()
        {
            _mainWindow.TaskbarItemInfo.Description = _shortInformation.Value;
        }
    }
}