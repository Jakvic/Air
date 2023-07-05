using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Air;

public abstract class View : UserControl
{
    private readonly bool _isDesign =
        (bool)DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue;

    protected View()
    {
        if (_isDesign is false)
        {
            return;
        }

        DependencyPropertyDescriptor.FromProperty(DataContextProperty, typeof(View))
            .AddValueChanged(this, OnDataContextChanged);
    }

    private void OnDataContextChanged(object? sender, EventArgs e)
    {
        if (DataContext is IViewModel viewModel)
        {
            viewModel.InnerOnInitialized(this);
        }
    }
}