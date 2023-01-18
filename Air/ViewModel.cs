using System;

namespace Air;

public abstract class ViewModel : Model
{
    protected Command GetCommand(Action action, Func<bool>? canAction = null)
    {
        return new Command(action, canAction);
    }

    protected Command<T> GetCommand<T>(Action<T?> action, Func<bool>? canAction = null)
    {
        return new Command<T>(action, canAction);
    }
}