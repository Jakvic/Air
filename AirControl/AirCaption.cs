using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using AirControl.Native;

namespace AirControl;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "LocalVariableHidesMember")]
public class AirCaption : ContentControl
{
    private const int BUTTONDOWN = 0x000000A1;
    private const uint CAPTION = 2;

    private Window? _window;


    private Grid? PART_Grid;

    static AirCaption()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(AirCaption), new FrameworkPropertyMetadata(typeof(AirCaption)));
    }

    public AirCaption()
    {
        CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, MinimizeWindow));
        CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, MaximizeWindow));
        CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, CloseWindow));
    }

    private void MinimizeWindow(object sender, ExecutedRoutedEventArgs e)
    {
        if (_window is null)
        {
            return;
        }

        SystemCommands.MinimizeWindow(_window);
    }

    private void MaximizeWindow(object? sender, ExecutedRoutedEventArgs? e)
    {
        if (_window is null)
        {
            return;
        }

        if (_window.WindowState is WindowState.Maximized)
        {
            _window.BorderThickness = new Thickness(0);
            _window.WindowStyle = WindowStyle.SingleBorderWindow;
            SystemCommands.RestoreWindow(_window);
        }
        else
        {
            SystemCommands.MaximizeWindow(_window);
            _window.BorderThickness = new Thickness(8);
        }
    }

    private void CloseWindow(object sender, ExecutedRoutedEventArgs e)
    {
        if (_window is null)
        {
            return;
        }

        SystemCommands.CloseWindow(_window);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _window = Window.GetWindow(this);
        _window.SizeChanged += OnSizeChanged;
        PART_Grid = GetTemplateChild("PART_Grid") as Grid;
        if (PART_Grid is null)
        {
            return;
        }

        PART_Grid.MouseLeftButtonDown += OnMouseLeftButtonDown;
    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (_window is null)
        {
            return;
        }

        if (_window.WindowState is WindowState.Maximized)
        {
            _window.BorderThickness = new Thickness(7.4);
            _window.WindowStyle = WindowStyle.SingleBorderWindow;
        }
        else
        {
            _window.BorderThickness = new Thickness(0);

        }
    }

    private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (PART_Grid is null)
        {
            return;
        }

        if (_window is null)
        {
            return;
        }

        if (e.ClickCount == 2)
        {
            MaximizeWindow(null, null);
        }
        else
        {
            var handle = new WindowInteropHelper(_window).Handle;

            NativeMethods.SendMessage(handle, BUTTONDOWN, (IntPtr)CAPTION, IntPtr.Zero);
        }
    }
}