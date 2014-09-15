using IISWorkerProcessLister.Core;
using IISWorkerProcessLister.Internal;
using MahApps.Metro.Controls;
using Microsoft.Web.Administration;
using System;
using System.Windows;
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
        private readonly ApplicationSettings _applicationSettings;
        private readonly IDataGridItem _dataGridItem;
        private readonly DispatcherTimer _dispatcherTimer = new DispatcherTimer();
        private readonly IWorkerProccessInformation _workerProcessInformation;

        public MainWindow()
        {
            InitializeComponent();
            Title = Properties.Resources.MainWindow_Title;
            _applicationSettings = new ApplicationSettings(this);
            _dataGridItem = new GetDataGridItem();
            _workerProcessInformation = new WorkerProcessInformation();

            GetWorkerProcesses();
            _dispatcherTimer.Tick += DispatcherTimerTick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            _dispatcherTimer.Start();
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                Hide();
            }

            base.OnStateChanged(e);
        }

        private void DispatcherTimerTick(object sender, EventArgs e)
        {
            GetWorkerProcesses();
        }

        private void GetWorkerProcesses()
        {
            var applicationPoolApplications = new GetApplicationPoolApplications();
            var applicationPoolSitesAndApplications =
                new ReturnApplicationPoolSitesAndApplications(applicationPoolApplications);
            var serverManager = new ServerManager();
            var workerProcessItem = new GetWorkerProcessItem();
            var itemsSource = new GetWorkerProcessItemsSource(applicationPoolSitesAndApplications, workerProcessItem,
                serverManager);
            WorkerProcessesDataGrid.ItemsSource = itemsSource.Value;
            _applicationSettings.SetBalloonTipText(_workerProcessInformation.Value);
            //_applicationSettings.SetHoverText(_workerProcesses.WorkerProcessShortInfo);
        }

        private void KillProcessClick(object sender, RoutedEventArgs e)
        {
            var dataGridItem = new GetDataGridItem();
            var workerProcessDataGridItem = new GetWorkerProcessItemByDataGridItem(dataGridItem, sender);
            var workerProcess = new CloseWorkerProcessByProcessId(workerProcessDataGridItem);
            workerProcess.Run();
            GetWorkerProcesses();
        }

        private void RecycleAppPoolClick(object sender, RoutedEventArgs e)
        {
            var dataGridItem = new GetDataGridItem();
            var workerProcessDataGridItem = new GetWorkerProcessItemByDataGridItem(dataGridItem, sender);
            var serverManager = new ServerManager();
            var workerProcess = new RecycleApplicationPool(workerProcessDataGridItem, serverManager);
            workerProcess.Run();
            GetWorkerProcesses();
        }
    }
}