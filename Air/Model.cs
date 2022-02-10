using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Air.Annotations;
#pragma warning disable CS8603
#pragma warning disable CS8625

namespace Air
{
    public abstract class Model : INotifyPropertyChanged
    {
        private Dictionary<string, object?> PropertyDict { get; }

        protected Model()
        {
            PropertyDict = new Dictionary<string, object?>();
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected T GetValue<T>(Func<T>? defaultValue = null, [CallerMemberName] string? propertyName = null)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return default;
            }

            if (!PropertyDict.ContainsKey(propertyName))
            {
                PropertyDict[propertyName] = defaultValue is null ? default : defaultValue.Invoke();
            }

            return (T) PropertyDict[propertyName]!;
        }

        protected bool SetValue<T>(T value, Action? callback = null, [CallerMemberName] string? propertyName = null)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return false;
            }

            if (PropertyDict.ContainsKey(propertyName) && Equals(PropertyDict[propertyName], value))
            {
                return false;
            }
            callback?.Invoke();
            PropertyDict[propertyName] = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected bool SetValue(Action? action, [CallerMemberName] string? propertyName = null)
        {
            if (action is null)
            {
                return false;
            }
            action.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        
    }
}