using IISWorkerProcessLister.Core;
using IISWorkerProcessLister.Internal;
using MahApps.Metro.Controls;
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
        private readonly DataGridUtilities _datagridUtilities;
        private readonly DispatcherTimer _dispatcherTimer = new DispatcherTimer();
        private readonly WorkerProcesses _workerProcesses;

        public MainWindow()
        {
            InitializeComponent();
            Title = Properties.Resources.MainWindow_Title;
            _applicationSettings = new ApplicationSettings(this);
            _datagridUtilities = new DataGridUtilities();

            _workerProcesses = new WorkerProcesses();

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
            WorkerProcessesDataGrid.ItemsSource = _workerProcesses.ItemsSource();
            _applicationSettings.SetBalloonTipText(_workerProcesses.WorkerProcessInfo);
            _applicationSettings.SetHoverText(_workerProcesses.WorkerProcessShortInfo);
        }

        private void KillProcessClick(object sender, RoutedEventArgs e)
        {
            var item = _datagridUtilities.GetDataGridItem(sender);

            _workerProcesses.CloseWorkerProcess(item);

            GetWorkerProcesses();
        }

        private void RecycleAppPoolClick(object sender, RoutedEventArgs e)
        {
            var item = _datagridUtilities.GetDataGridItem(sender);

            _workerProcesses.RecycleApplicationPool(item);

            GetWorkerProcesses();
        }
    }
}