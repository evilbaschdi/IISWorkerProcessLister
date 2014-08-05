using IISWorkerProcessLister.Internal;
using MahApps.Metro.Controls;
using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace IISWorkerProcessLister
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    // ReSharper disable RedundantExtendsListEntry
    public partial class MainWindow : MetroWindow
        // ReSharper restore RedundantExtendsListEntry
    {
        private readonly DispatcherTimer _dispatcherTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            StartMinimized();
            GetWorkerProcesses();
            _dispatcherTimer.Tick += DispatcherTimerTick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            _dispatcherTimer.Start();
        }

        public static string WorkerProcessShortInfo { get; set; }

        private void StartMinimized()
        {
            MinimizeToTray.Enable(this);
            //WindowState = WindowState.Minimized;
        }

        private void DispatcherTimerTick(object sender, EventArgs e)
        {
            GetWorkerProcesses();
        }

        private void GetWorkerProcesses()
        {
            var serverManager = new ServerManager();
            var workerProcesses = serverManager.WorkerProcesses;

            var itemsSource = new BindingList<WorkerProcessItem>();
            itemsSource.Clear();

            foreach (var process in workerProcesses)
            {
                var appPoolName = process.AppPoolName;
                var processId = process.ProcessId;

                WorkerProcessShortInfo += string.Format("App Pool: {0} | Process Id: {1}{2}", appPoolName, processId,
                    Environment.NewLine);

                var workerProcessItem = new WorkerProcessItem
                {
                    ProcessId = process.ProcessId,
                    AppPoolName = process.AppPoolName,
                    Applications = ReturnApplicationPoolSitesAndApplications(serverManager.Sites, appPoolName),
                    State = process.State.ToString()
                };
                itemsSource.Add(workerProcessItem);
            }

            WorkerProcessesDataGrid.ItemsSource = itemsSource;
        }

        private string ReturnApplicationPoolSitesAndApplications(IEnumerable<Site> sites, string appPoolName)
        {
            var applicationPoolApplications = "";

            foreach (var site in sites)
            {
                foreach (var application in site.Applications)
                {
                    if (application.ApplicationPoolName == appPoolName)
                    {
                        applicationPoolApplications += string.Format("{0}{1}, ", site.Name, application.Path);
                    }
                }
                //applicationPoolApplications = applicationPoolApplications.Replace(site.Name + "/,", "");
            }

            return applicationPoolApplications.Remove(applicationPoolApplications.Trim().Length - 1, 1);
        }

        private void KillProcessClick(object sender, RoutedEventArgs e)
        {
            //Get the clicked MenuItem
            var menuItem = (MenuItem) sender;

            //Get the ContextMenu to which the menuItem belongs
            var contextMenu = (ContextMenu) menuItem.Parent;

            //Find the placementTarget
            var item = (DataGrid) contextMenu.PlacementTarget;

            //Get the underlying item, that you cast to your object that is bound
            //to the DataGrid (and has subject and state as property)
            var workerProcessItem = (WorkerProcessItem) item.SelectedCells[0].Item;

            KillOpenProcessById(workerProcessItem.ProcessId);

            GetWorkerProcesses();
        }

        private static void KillOpenProcessById(int processId)
        {
            var process = Process.GetProcessById(processId);

            process.CloseMainWindow();
            process.WaitForExit(10000);

            if (!process.HasExited)
            {
                process.Kill();
            }
        }
    }
}