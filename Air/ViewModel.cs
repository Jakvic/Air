using System;
using System.Runtime.CompilerServices;

namespace Air
{
    public abstract class ViewModel : Model
    {
        protected Command GetCommand(Action action, Func<bool>? canAction = null
        ,[CallerMemberName] string? propertyName = null)
        {
            return GetValue(() => new Command(action, canAction), propertyName);
        }
    }
}