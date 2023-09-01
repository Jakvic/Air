using System.Threading.Tasks;
using System.Windows;

namespace Air;

public abstract class WindowModel<TUserControl> : ViewModel<TUserControl> where TUserControl : View, new()
{
    private Window? _current;
    private TaskCompletionSource? _taskCompletionSource;
    public abstract string Title { get; }

    public bool Ok { get; private set; }

    public Command CloseCommand => GetCommand(async () =>
    {
        Ok = true;
        if (_current is null)
        {
            return;
        }

        await UIThread.InvokeAsync(() => _current.Close()).ConfigureAwait(false);
    });

    protected virtual Window CreateWindowOverride()
    {
        return new Window();
    }

    public Window CreateWindow()
    {
        var window = CreateWindowOverride();
        window.Title = Title;
        window.Content = new ViewModelHost(this);
        OnWindowInitialized(window);
        return window;
    }

    protected virtual void OnWindowInitialized(Window window)
    {
        _current = window;
        _current.Closed += delegate { _taskCompletionSource?.SetResult(); };
    }

    public async Task ShowAsync()
    {
        _taskCompletionSource = new TaskCompletionSource();
        await UIThread.InvokeAsync(() => { CreateWindow().Show(); }).ConfigureAwait(false);
        await _taskCompletionSource.Task.ConfigureAwait(false);
    }

    public async Task ShowModelAsync(Window owner)
    {
        _taskCompletionSource = new TaskCompletionSource();
        await UIThread.InvokeAsync(() =>
        {
            var window = CreateWindow();
            window.Owner = owner;
            window.ShowDialog();
        }).ConfigureAwait(false);

        await _taskCompletionSource.Task.ConfigureAwait(false);
    }
}