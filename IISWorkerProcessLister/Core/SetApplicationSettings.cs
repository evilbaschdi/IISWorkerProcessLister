using System.Windows;
using IISWorkerProcessLister.Properties;

namespace IISWorkerProcessLister.Core
{
    /// <summary>
    /// </summary>
    public class SetApplicationSettings : IApplicationSettings
    {
        private readonly MainWindow _mainWindow;

        /// <summary>
        /// </summary>
        /// <param name="mainWindow"></param>
        /// <param name="ni"></param>
        public SetApplicationSettings(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        /// <summary>
        /// </summary>
        public void Run()
        {
            _mainWindow.Title = Resources.MainWindow_Title;
            _mainWindow.RecycleAppPoolMenuItem.Header = Resources.RecycleAppPoolMenuItem_Header;
            _mainWindow.KillProcessMenuItem.Header = Resources.KillProcessMenuItem_Header;
            _mainWindow.StopAppPoolMenuItem.Header = Resources.StopAppPoolMenuItem_Header;
            StartMinimized();
        }

        private void StartMinimized()
        {
            _mainWindow.WindowState = WindowState.Minimized;
        }
    }
}