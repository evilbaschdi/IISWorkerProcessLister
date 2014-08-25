using IISWorkerProcessLister.Core;
using IISWorkerProcessLister.Internal;
using MahApps.Metro.Controls;
using System;
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
        private readonly WorkerProcesses _workerProcesses;
        private readonly ApplicationSettings _applicationSettings;

        public MainWindow()
        {
            
            InitializeComponent();
            //_applicationSettings = new ApplicationSettings(this);
            //_applicationSettings.StartMinimized();

            _workerProcesses = new WorkerProcesses();

            GetWorkerProcesses();
            _dispatcherTimer.Tick += DispatcherTimerTick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            _dispatcherTimer.Start();
            
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }
        
        private void DispatcherTimerTick(object sender, EventArgs e)
        {
            GetWorkerProcesses();
        }

        private void GetWorkerProcesses()
        {
            WorkerProcessesDataGrid.ItemsSource = _workerProcesses.ItemsSource();
        }
        
        private void KillProcessClick(object sender, RoutedEventArgs e)
        {
            //Get the clicked MenuItem
            var menuItem = (MenuItem) sender;

            //Get the ContextMenu to which the menuItem belongs
            var contextMenu = (ContextMenu) menuItem.Parent;

            //Find the placementTarget
            var item = (DataGrid) contextMenu.PlacementTarget;
            
            _workerProcesses.CloseWorkerProcess(item);

            GetWorkerProcesses();
        }

    }
}