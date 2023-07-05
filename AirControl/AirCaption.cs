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
    private const int NCLBUTTONDOWN = 0x000000A1;
    private const uint CAPTION = 2;

    public static readonly DependencyProperty SystemButtonWidthProperty = DependencyProperty.Register(
        nameof(SystemButtonWidth), typeof(double), typeof(AirCaption), new PropertyMetadata(default(double)));

    private Grid? PART_Grid;

    static AirCaption()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(AirCaption), new FrameworkPropertyMetadata(typeof(AirCaption)));
    }

    public double SystemButtonWidth
    {
        get => (double)GetValue(SystemButtonWidthProperty);
        set => SetValue(SystemButtonWidthProperty, value);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        PART_Grid = GetTemplateChild("PART_Grid") as Grid;
        if (PART_Grid is null) return;

        PART_Grid.MouseLeftButtonDown += OnMouseLeftButtonDown;
    }

    private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (PART_Grid is null) return;

        var window = Window.GetWindow(PART_Grid);
        if (window is null) return;

        var handle = new WindowInteropHelper(window).Handle;

        NativeMethods.SendMessage(handle, NCLBUTTONDOWN, (IntPtr)CAPTION, IntPtr.Zero);
    }
}