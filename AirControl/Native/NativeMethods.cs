using System;
using System.Runtime.InteropServices;

namespace AirControl.Native;

public static class NativeMethods
{
    [DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);
}