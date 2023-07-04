using System.Windows;
using Air.WPFDemo.Services;

namespace Air.WPFDemo;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var mainModel = new Main_Model().CreateWindow();

        Service.Register(new AppService(mainModel));
        MainWindow = mainModel;
        MainWindow.Show();
    }
}