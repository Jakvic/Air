using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Air
{
    public class Person : ViewModel
    {
        public string Name
        {
            get => GetValue(() => string.Empty);
            set => SetValue(value);
        }

        public string Gender
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public Command Eat => GetCommand(delegate
        {
            Task.Run(async delegate
            {
                Debug.WriteLine("Am Eating..." + DateTime.Now);
                await Task.Delay(2000);
                Debug.WriteLine("Done" + DateTime.Now);
            });
            //DeadLockDemo.Test();
           //DeadLockDemo.TestNotFullyAsync();
           DeadLockDemo.FuncAsync();
        });
    }
}