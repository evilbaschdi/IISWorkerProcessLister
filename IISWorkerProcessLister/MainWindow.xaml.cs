using System;
using System.Windows;
using System.Windows.Threading;
using EvilBaschdi.CoreExtended.Metro;
using EvilBaschdi.CoreExtended.Mvvm;
using EvilBaschdi.CoreExtended.Mvvm.View;
using EvilBaschdi.CoreExtended.Mvvm.ViewModel;
using IISWorkerProcessLister.Core;
using IISWorkerProcessLister.Internal;
using IISWorkerProcessLister.Main;
using MahApps.Metro.Controls;
using Microsoft.Web.Administration;

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
        private readonly IMain _main;
        private readonly IThemeManagerHelper _themeManagerHelper;

        /// <inheritdoc />
        public MainWindow()
        {
            InitializeComponent();
            _themeManagerHelper = new ThemeManagerHelper();
            IApplicationStyle applicationStyle = new ApplicationStyle(_themeManagerHelper);
            applicationStyle.Load(true);


            _dispatcherTimer.Tick += DispatcherTimerTick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            _dispatcherTimer.Start();
            var applicationSettings = new SetApplicationSettings(this);
            applicationSettings.Run();

            _main = new Execute(this);
            _main.Run();
        }

        /// <summary>
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            _dispatcherTimer.Tick -= DispatcherTimerTick;
            base.OnClosed(e);
        }


        private void DispatcherTimerTick(object sender, EventArgs e)
        {
            _main.Run();
        }

        private void KillProcessClick(object sender, RoutedEventArgs e)
        {
            var dataGridItem = new GetDataGridItem();
            var workerProcessDataGridItem = new GetWorkerProcessItemByDataGridItem(dataGridItem, sender);
            var workerProcess = new CloseWorkerProcessByProcessId(workerProcessDataGridItem);
            workerProcess.Run();
            _main.Run();
        }

        private void RecycleAppPoolClick(object sender, RoutedEventArgs e)
        {
            var dataGridItem = new GetDataGridItem();
            var workerProcessDataGridItem = new GetWorkerProcessItemByDataGridItem(dataGridItem, sender);
            var serverManager = new ServerManager();
            var workerProcess = new RecycleApplicationPool(workerProcessDataGridItem, serverManager);
            workerProcess.Run();
            _main.Run();
        }

        private void StopAppPoolMenuItem_OnClickAppPoolClick(object sender, RoutedEventArgs e)
        {
            var dataGridItem = new GetDataGridItem();
            var workerProcessDataGridItem = new GetWorkerProcessItemByDataGridItem(dataGridItem, sender);
            var serverManager = new ServerManager();
            var workerProcess = new StopApplicationPool(workerProcessDataGridItem, serverManager);
            workerProcess.Run();
            _main.Run();
        }

        private void AboutWindowClick(object sender, RoutedEventArgs e)
        {
            var assembly = typeof(MainWindow).Assembly;
            IAboutWindowContent aboutWindowContent = new AboutWindowContent(assembly, $@"{AppDomain.CurrentDomain.BaseDirectory}\server.png");

            var aboutWindow = new AboutWindow
                              {
                                  DataContext = new AboutViewModel(aboutWindowContent, _themeManagerHelper)
                              };

            aboutWindow.ShowDialog();
        }
    }
}