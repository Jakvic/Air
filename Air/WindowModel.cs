using System.Threading.Tasks;
using System.Windows;

namespace Air;

public abstract class WindowModel<TUserControl> : ViewModel<TUserControl> where TUserControl : View, new()
{
    public abstract string Title { get; }

    protected virtual Window CreateWindowOverride()
    {
        return new();
    }

    public Window CreateWindow()
    {
        var window = CreateWindowOverride();
        window.Title = Title;
        window.Content = new ViewModelHost(this);
        OnWindowInitialized(window);
        return window;
    }

    Window? _current;
    TaskCompletionSource? _taskCompletionSource;

    protected virtual void OnWindowInitialized(Window window)
    {
        _current = window;
        _current.Closed += delegate
        {
            _taskCompletionSource?.SetResult();
        };
    }


    public async Task ShowAsync()
    {
        _taskCompletionSource = new();
        await UIThread.InvokeAsync(() => { CreateWindow().Show(); }).ConfigureAwait(false);
        await _taskCompletionSource.Task.ConfigureAwait(false);
    }

    public async Task ShowModelAsync(Window owner)
    {
        _taskCompletionSource = new();
        await UIThread.InvokeAsync(() =>
        {
            var window = CreateWindow();
            window.Owner = owner;
            window.ShowDialog();
        }).ConfigureAwait(false);

        await _taskCompletionSource.Task.ConfigureAwait(false);
    }

    public bool Ok { get; private set; }

    public Command CloseCommand => GetCommand(async () =>
    {
        Ok = true;
        if (_current is null)
        {
            return;
        }

        await UIThread.InvokeAsync(() => _current.Close()).ConfigureAwait(false); ;
    });

}
