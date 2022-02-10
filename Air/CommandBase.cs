using System;
using System.Windows.Input;

namespace Air
{
    public abstract class CommandBase:ICommand
    {
        public abstract bool CanExecute(object? parameter);

        public abstract void Execute(object? parameter);

        public event EventHandler? CanExecuteChanged;

        protected void OnChanged()
        {
            CanExecuteChanged?.Invoke(this,EventArgs.Empty);
        }
    }
}