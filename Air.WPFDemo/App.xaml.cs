using System.Windows;
using System.Windows.Shell;
using Air.WPFDemo.Services;

namespace Air.WPFDemo;

public partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var mainModel = new Main_Model().CreateWindow();
        mainModel.WindowStyle = WindowStyle.SingleBorderWindow;
        //WindowChrome.SetWindowChrome(mainModel, new WindowChrome
        //{
        //    CaptionHeight = 0,
        //    UseAeroCaptionButtons = true,
        //    GlassFrameThickness = new Thickness(10)
        //});

        //如果这里不指定Application.MainWindow，WPF默认会用NavigationWindow作为父窗体
        Service.Register(new AppService(mainModel));
        MainWindow = mainModel;
        MainWindow.Show();
    }
}