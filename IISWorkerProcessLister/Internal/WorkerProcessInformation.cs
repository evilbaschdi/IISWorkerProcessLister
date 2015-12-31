using System;
using System.Windows.Forms;
using IISWorkerProcessLister.Core;

namespace IISWorkerProcessLister.Internal
{
    /// <summary>
    /// </summary>
    public class WorkerProcessInformation : IWorkerProcessInformation
    {
        private readonly IExtendedInformation _extendedInformation;
        private readonly NotifyIcon _notifyIcon;
        private readonly IShortInformation _shortInformation;

        /// <summary>
        /// </summary>
        /// <param name="notifyIcon"></param>
        /// <param name="extendedInformation"></param>
        /// <param name="shortInformation"></param>
        public WorkerProcessInformation(NotifyIcon notifyIcon, IExtendedInformation extendedInformation,
            IShortInformation shortInformation)
        {
            if(notifyIcon == null)
            {
                throw new ArgumentNullException(nameof(notifyIcon));
            }
            _notifyIcon = notifyIcon;
            _extendedInformation = extendedInformation;
            _shortInformation = shortInformation;
        }

        /// <summary>
        /// </summary>
        public void Set()
        {
            _notifyIcon.BalloonTipText = _extendedInformation.Value;
            _notifyIcon.Text = _shortInformation.Value.MaxLengthCutRight(63);
        }
    }
}