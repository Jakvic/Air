using System;
using System.Linq;
using System.Windows;

namespace Air;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var singleOrDefault = assemblies.SingleOrDefault(x => x.GetName().Name == "Air");
        var enumerable = singleOrDefault.GetTypes().FirstOrDefault(x => x.IsAbstract && x.Name == "ViewModel");
        foreach (var type in singleOrDefault.GetTypes().Where(x => x.IsSubclassOf(enumerable)))
        {
        }
    }
}