using System.Windows;
using IISWorkerProcessLister.Properties;

namespace IISWorkerProcessLister.Core;

/// <summary>
/// </summary>
public class SetApplicationSettings : IApplicationSettings
{
    private readonly MainWindow _mainWindow;

    /// <summary>
    /// </summary>
    /// <param name="mainWindow"></param>
    public SetApplicationSettings(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
    }

    /// <summary>
    /// </summary>
    public void Run()
    {
        _mainWindow.SetCurrentValue(Window.TitleProperty, Resources.MainWindow_Title);
        StartMinimized();
    }

    private void StartMinimized()
    {
        _mainWindow.SetCurrentValue(Window.WindowStateProperty, WindowState.Minimized);
    }
}