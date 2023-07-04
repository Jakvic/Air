using System.Windows;
using System.Windows.Markup;

[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]

/*
 This can ignore the prefix when this `proj` referenced in xaml 
 for example: old -> `<controls:AirButton/>`, new: `<AirButton/>`
 very nice :)
 */
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "Air")]