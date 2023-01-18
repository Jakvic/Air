using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Air;

public class Person : ViewModel
{
    private string _gender;
    private string _name;

    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    public string Gender
    {
        get => _gender;
        set => SetField(ref _gender, value);
    }

    public Command Eat => GetCommand(async () =>
    {
        Debug.WriteLine("Am Eating..." + DateTime.Now);
        await Task.Delay(2000).ConfigureAwait(false);
        Debug.WriteLine("Done" + DateTime.Now);
        //DeadLockDemo.Test();
        //DeadLockDemo.TestNotFullyAsync();
    });
}