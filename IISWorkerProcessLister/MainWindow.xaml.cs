using System.Windows;
using System.Windows.Threading;
using EvilBaschdi.About.Core;
using EvilBaschdi.About.Core.Models;
using EvilBaschdi.About.Wpf;
using EvilBaschdi.Core;
using EvilBaschdi.Core.Wpf;
using IISWorkerProcessLister.Core;
using IISWorkerProcessLister.Internal;
using IISWorkerProcessLister.Main;
using MahApps.Metro.Controls;
using Microsoft.Web.Administration;

namespace IISWorkerProcessLister;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>

// ReSharper disable RedundantExtendsListEntry
public partial class MainWindow : MetroWindow
    // ReSharper restore RedundantExtendsListEntry
{
    private readonly DispatcherTimer _dispatcherTimer = new();
    private readonly IMain _main;

    /// <inheritdoc />
    public MainWindow()
    {
        InitializeComponent();

        IApplicationStyle applicationStyle = new ApplicationStyle();
        IApplicationLayout applicationLayout = new ApplicationLayout();
        applicationStyle.Run();
        applicationLayout.RunFor((true, false));

        _dispatcherTimer.Tick += DispatcherTimerTick;
        _dispatcherTimer.Interval = new(0, 0, 10);
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
        ICurrentAssembly currentAssembly = new CurrentAssembly();
        IAboutContent aboutContent = new AboutContent(currentAssembly);
        IAboutViewModel aboutModel = new AboutViewModel(aboutContent);
        IApplyMicaBrush applyMicaBrush = new ApplyMicaBrush();
        IApplicationLayout applicationLayout = new ApplicationLayout();
        var aboutWindow = new AboutWindow(aboutModel, applicationLayout, applyMicaBrush);

        aboutWindow.ShowDialog();
    }
}