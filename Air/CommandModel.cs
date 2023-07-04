using System;

namespace Air;

public abstract class CommandModel : Model
{
    public Command GetCommand(Action action, Predicate<object?>? predicate = default)
    {
        return new Command(action, predicate);
    }

    public Command<T> GetCommand<T>(Action<T?> action, Predicate<object?>? predicate = default)
    {
        return new Command<T>(action, predicate);
    }
}