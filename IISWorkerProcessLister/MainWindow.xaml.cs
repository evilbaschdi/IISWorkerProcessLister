﻿using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using EvilBaschdi.Core.Application;
using EvilBaschdi.Core.Wpf;
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
        private readonly IMetroStyle _style;
        private readonly IMain _main;
        private readonly int _overrideProtection;
        private readonly DispatcherTimer _dispatcherTimer = new DispatcherTimer();


        /// <summary>
        ///     MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            ISettings coreSettings = new CoreSettings(Properties.Settings.Default);
            IThemeManagerHelper themeManagerHelper = new ThemeManagerHelper();
            _style = new MetroStyle(this, Accent, ThemeSwitch, coreSettings, themeManagerHelper);
            _style.Load(true);
            var linkerTime = Assembly.GetExecutingAssembly().GetLinkerTime();
            LinkerTime.Content = linkerTime.ToString(CultureInfo.InvariantCulture);

            _dispatcherTimer.Tick += DispatcherTimerTick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            _dispatcherTimer.Start();
            var applicationSettings = new SetApplicationSettings(this);
            applicationSettings.Run();

            _main = new Execute(this);
            _main.Run();

            _overrideProtection = 1;
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

        #region Flyout

        private void ToggleSettingsFlyoutClick(object sender, RoutedEventArgs e)
        {
            ToggleFlyout(0);
        }

        private void ToggleFlyout(int index, bool stayOpen = false)
        {
            var activeFlyout = (Flyout) Flyouts.Items[index];
            if (activeFlyout == null)
            {
                return;
            }

            foreach (
                var nonactiveFlyout in
                Flyouts.Items.Cast<Flyout>()
                       .Where(nonactiveFlyout => nonactiveFlyout.IsOpen && nonactiveFlyout.Name != activeFlyout.Name))
            {
                nonactiveFlyout.IsOpen = false;
            }

            if (activeFlyout.IsOpen && stayOpen)
            {
                activeFlyout.IsOpen = true;
            }
            else
            {
                activeFlyout.IsOpen = !activeFlyout.IsOpen;
            }
        }

        #endregion Flyout

        #region MetroStyle

        private void SaveStyleClick(object sender, RoutedEventArgs e)
        {
            if (_overrideProtection == 0)
            {
                return;
            }
            _style.SaveStyle();
        }

        private void Theme(object sender, EventArgs e)
        {
            if (_overrideProtection == 0)
            {
                return;
            }
            var routedEventArgs = e as RoutedEventArgs;
            if (routedEventArgs != null)
            {
                _style.SetTheme(sender, routedEventArgs);
            }
            else
            {
                _style.SetTheme(sender);
            }
        }

        private void AccentOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_overrideProtection == 0)
            {
                return;
            }
            _style.SetAccent(sender, e);
        }

        #endregion MetroStyle
    }
}