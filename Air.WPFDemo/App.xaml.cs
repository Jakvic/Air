using System.Windows;
using Air.WPFDemo.Services;

namespace Air.WPFDemo;
public partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var mainModel = new Main_Model().CreateWindow();

        //如果这里不指定Application.MainWindow，WPF默认会用NavigationWindow作为父窗体
        Service.Register(new AppService(mainModel));
        MainWindow = mainModel;
        MainWindow.Show();
    }
}