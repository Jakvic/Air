using System;
using System.Windows;

namespace Air;

public class AirApp : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
    }
}