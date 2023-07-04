using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Air;

//INotifyPropertyChanged是如何被加载、触发的  https://zhuanlan.zhihu.com/p/464381909

//PropertyChangedEventManager
//https://referencesource.microsoft.com/#WindowsBase/Base/System/ComponentModel/PropertyChangedEventManager.cs,8862db7b374f076b
public abstract class Model : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
        {
            return false;
        }

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected void OnPropertyChanged(string? propertyName)
    {
        PropertyChanged?.Invoke(this, new(propertyName));
    }

}
