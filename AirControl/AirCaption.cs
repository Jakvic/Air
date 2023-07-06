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

    private void MaximizeWindow(object sender, ExecutedRoutedEventArgs e)
    {
        if (_window is null)
        {
            return;
        }

        if (_window.WindowState is WindowState.Maximized)
        {
            SystemCommands.RestoreWindow(_window);
        }
        else
        {
            SystemCommands.MaximizeWindow(_window);
            //_window.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
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
        PART_Grid = GetTemplateChild("PART_Grid") as Grid;
        if (PART_Grid is null)
        {
            return;
        }

        PART_Grid.MouseLeftButtonDown += OnMouseLeftButtonDown;
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
            if (_window.WindowState is WindowState.Normal)
            {
                _window.WindowState = WindowState.Maximized;
                
                //_window.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            }
            else
            {
                _window.WindowState = WindowState.Normal;
            }
        }
        else
        {
            var handle = new WindowInteropHelper(_window).Handle;

            NativeMethods.SendMessage(handle, BUTTONDOWN, (IntPtr)CAPTION, IntPtr.Zero);
        }
    }
}