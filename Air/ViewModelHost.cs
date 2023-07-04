using System;
using System.Windows;
using System.Windows.Controls;

namespace Air;

public sealed class ViewModelHost : ContentControl
{
    
    public ViewModelHost(IViewModel viewModel)
    {
        ViewModel = viewModel;
    }

    public IViewModel? ViewModel
    {
        get { return (IViewModel)GetValue(ViewModelProperty); }
        set { SetValue(ViewModelProperty, value); }
    }

    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register("ViewModel", typeof(IViewModel), typeof(ViewModelHost), new(null, OnViewModelChanged));


    private static void OnViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not ViewModelHost viewModelHost)
        {
            return;
        }

        viewModelHost.Content = (e.NewValue as IViewModel)?.CreateView();
    }

    public Type? ViewModelType
    {
        get { return (Type)GetValue(MyPropertyProperty); }
        set { SetValue(MyPropertyProperty, value); }
    }

    public static readonly DependencyProperty MyPropertyProperty =
        DependencyProperty.Register("MyProperty", typeof(int), typeof(ViewModelHost), new(null, OnViewModelTypeChanged));

    private static void OnViewModelTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not ViewModelHost viewModelHost)
        {
            return;
        }

        if (e.NewValue is not Type type)
        {
            viewModelHost.ViewModel = null;
        }
        else if (Activator.CreateInstance(type) is IViewModel viewModel)
        {
            viewModelHost.ViewModel = viewModel;
        }
        else
        {
            viewModelHost.ViewModel = null;
        }
    }

}