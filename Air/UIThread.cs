using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Air;

public static class UIThread
{
    private static Dispatcher Dispatcher => Application.Current.Dispatcher;

    public static void Invoke(Action action, DispatcherPriority priority = default)
    {
        Dispatcher.Invoke(action, priority);
    }

    public static T Invoke<T>(Func<T> func, DispatcherPriority priority = default)
    {
        return Dispatcher.Invoke(func, priority);
    }

    public static async Task InvokeAsync(Action action, DispatcherPriority priority = default)
    {
        await Dispatcher.InvokeAsync(action, priority);
    }

    public static async Task<T> InvokeAsync<T>(Func<T> func, DispatcherPriority priority = default)
    {
        return await Dispatcher.InvokeAsync(func, priority);
    }
}
