using System.Windows;

namespace Air.WPFDemo.Services;

public class AppService
{
    public AppService(Window mainWindow)
    {
        Window = mainWindow;
    }

    public Window Window { get; }
}