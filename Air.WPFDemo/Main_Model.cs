using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Air.WPFDemo;

public class Main_Model : WindowModel<Main>
{
    private string _gender = "gg";
    private string _name = "name";

    public override string Title => "Main";
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