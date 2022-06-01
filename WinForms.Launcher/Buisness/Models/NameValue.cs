using WinForms.Launcher.Buisness.Common;

namespace WinForms.Launcher.Buisness.Models;

internal class NameValue : ViewModelBase
{
    private string _value;

    public NameValue(string name, string value = "")
    {
        Name = name;
        _value = value;
    }

    public string Name { get; }

    public string Value 
    {
        get => _value;
        set => SetWhenNotEqual(value);
    }
}
