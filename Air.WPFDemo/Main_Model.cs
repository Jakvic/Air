using System;
using System.Windows;
using Air.WPFDemo.Services;

namespace Air.WPFDemo;

public class Main_Model : WindowModel<Main>
{
    private readonly Lazy<AppService> _appService = Service.GetLazy<AppService>();

    private string? _message;
    protected override string Title => "Main";

    public string? Message
    {
        get => _message;
        private set => SetField(ref _message, value);
    }

    protected override void OnInitialized(Main view)
    {
        base.OnInitialized(view);
        Message = "The Air.WPFDemo";
    }

    protected override void OnWindowInitialized(Window window)
    {
        base.OnWindowInitialized(window);
        window.Width = 800;
        window.Height = 560;
        window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
    }
}