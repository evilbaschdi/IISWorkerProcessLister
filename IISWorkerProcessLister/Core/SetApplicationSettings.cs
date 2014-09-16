using IISWorkerProcessLister.Properties;
using System;
using System.Drawing;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;

namespace IISWorkerProcessLister.Core
{
    public class SetApplicationSettings : IApplicationSettings
    {
        private readonly MainWindow _mainWindow;
        private NotifyIcon _ni;

        public SetApplicationSettings(MainWindow mainWindow, NotifyIcon ni)
        {
            _mainWindow = mainWindow;
            _ni = ni;
        }

        public void Run()
        {
            _mainWindow.Title = Resources.MainWindow_Title;
            StartMinimized();
            _ni.ContextMenu = NotifyIconContextMenu();
            _ni.DoubleClick += NotifyIcon_DoubleClick;
            //_ni.Click += (sender, args) => _ni.ShowBalloonTip(10);
        }

        public void StartMinimized()
        {
            _ni = new NotifyIcon
            {
                Icon = Icon.ExtractAssociatedIcon(Assembly.GetEntryAssembly().Location),
                BalloonTipTitle = Resources.ApplicationSettings_StartMinimized_BalloonTipTitle,
                Visible = true
            };

            _mainWindow.Hide();
        }

        private ContextMenu NotifyIconContextMenu()
        {
            var contextMenu = new ContextMenu();

            var restore = new MenuItem
            {
                Index = 1,
                Text = Resources.ApplicationSettings_NotifyIconContextMenu_Open
            };

            restore.Click += ContextMenuItemRestore_Click;

            contextMenu.MenuItems.AddRange(new[] { restore });

            return contextMenu;
        }

        private void ContextMenuItemClose_Click(object sender, EventArgs e)
        {
            // Will Close Your Application
            _ni.Dispose();
            //Application.Exit();
        }

        private void ContextMenuItemRestore_Click(object sender, EventArgs e)
        {
            //Will Restore Your Application
            _mainWindow.Show();
            _mainWindow.WindowState = WindowState.Normal;
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            //Will Restore Your Application
            _mainWindow.Show();
            _mainWindow.WindowState = WindowState.Normal;
        }
    }
}