using System;
using System.Collections.ObjectModel;
using System.Windows;
using Air.WPFDemo.Services;

namespace Air.WPFDemo;

public class Main_Model : WindowModel<Main>
{
    private readonly Lazy<AppService> _appService = Service.GetLazy<AppService>();

    private string? _message;
    public override string Title => "Main";

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
        window.Width = 1000;
        window.Height = 760;
        window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
    }


    public ObservableCollection<string> Names =>
        new()
        {
            "Archer",
            "White",
            "Paul Desmond",
            "Brawler"
        };

    private Table_Model _tableModel = new();

    public Table_Model TableModel
    {
        get => _tableModel;
        set => SetField(ref _tableModel, value);
    }
}