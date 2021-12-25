using System.Windows;
using System.Windows.Markup;

[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
// this can remove the prefix of controls in this proj
// for example: old time -> `controls:AirButton`, now: AirButton
// very nice :)
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "AirControl")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "AirControl.Convertors")]