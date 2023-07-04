using System;
using System.Windows.Input;

namespace Air;

public class Command :ICommand
{
    private readonly Action _action;
    private readonly Predicate<object?>? _canExecute;

    public Command(Action action,Predicate<object?>? canExecute)
    {
        _action = action;
        _canExecute = canExecute;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return _canExecute?.Invoke(parameter) is not false;
    }

    public void Execute(object? parameter)
    {
        _action();
    }
}

public class Command<T>
{
    private readonly Action<T?> _action;
    private readonly Predicate<object?>? _canExecute;

    public Command(Action<T?> action, Predicate<object?>? canExecute)
    {
        _action = action;
        _canExecute = canExecute;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return _canExecute?.Invoke(parameter) is not false;
    }

    public void Execute(object? parameter)
    {
        _action?.Invoke((T?)parameter);
    }
}
