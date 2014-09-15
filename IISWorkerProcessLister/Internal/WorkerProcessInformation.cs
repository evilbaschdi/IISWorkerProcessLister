using IISWorkerProcessLister.Core;
using System;
using System.Windows.Forms;

namespace IISWorkerProcessLister.Internal
{
    public class WorkerProcessInformation : IWorkerProcessInformation
    {
        private readonly IExtendedInformation _extendedInformation;
        private readonly NotifyIcon _notifyIcon;
        private readonly IShortInformation _shortInformation;

        public WorkerProcessInformation(NotifyIcon notifyIcon, IExtendedInformation extendedInformation,
            IShortInformation shortInformation)
        {
            if (notifyIcon == null)
            {
                throw new ArgumentNullException("notifyIcon");
            }
            _notifyIcon = notifyIcon;
            _extendedInformation = extendedInformation;
            _shortInformation = shortInformation;
        }

        public void Set()
        {
            _notifyIcon.BalloonTipText = _extendedInformation.Value;
            _notifyIcon.Text = _shortInformation.Value.MaxLengthCutRight(63);
        }
    }
}