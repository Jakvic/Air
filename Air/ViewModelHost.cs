using System;
using System.Windows;
using System.Windows.Controls;

namespace Air;

public sealed class ViewModelHost : ContentControl
{
    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register(nameof(ViewModel), typeof(IViewModel), typeof(ViewModelHost),
            new PropertyMetadata(null, OnViewModelChanged));

    public static readonly DependencyProperty ViewModelTypeProperty =
        DependencyProperty.Register(nameof(ViewModelType), typeof(Type), typeof(ViewModelHost),
            new PropertyMetadata(null, OnViewModelTypeChanged));

    public ViewModelHost(IViewModel viewModel)
    {
        ViewModel = viewModel;
    }

    public IViewModel? ViewModel
    {
        get => (IViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    public Type? ViewModelType
    {
        get => (Type)GetValue(ViewModelTypeProperty);
        set => SetValue(ViewModelTypeProperty, value);
    }


    private static void OnViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not ViewModelHost viewModelHost) return;

        viewModelHost.Content = (e.NewValue as IViewModel)?.CreateView();
    }

    private static void OnViewModelTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not ViewModelHost viewModelHost) return;

        if (e.NewValue is not Type type)
            viewModelHost.ViewModel = null;
        else if (Activator.CreateInstance(type) is IViewModel viewModel)
            viewModelHost.ViewModel = viewModel;
        else
            viewModelHost.ViewModel = null;
    }
}