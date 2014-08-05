using System;
using System.Reflection;
using System.Windows;

namespace IISWorkerProcessLister.Core
{
    public class ApplicationSettings
    {
        private readonly MainWindow _mainWindow;

        public ApplicationSettings(MainWindow mainWindow)
        {

            _mainWindow = mainWindow;
            StartMinimized();
        }

        public void StartMinimized()
        {
            var ni = new System.Windows.Forms.NotifyIcon
            {
                Icon = System.Drawing.Icon.ExtractAssociatedIcon(Assembly.GetEntryAssembly().Location),
                Visible = true
            };
            ni.DoubleClick +=
                delegate(object sender, EventArgs args)
                {
                    _mainWindow.Show();
                    _mainWindow.WindowState = WindowState.Normal;
                };
            _mainWindow.Hide();

        }
    }
}