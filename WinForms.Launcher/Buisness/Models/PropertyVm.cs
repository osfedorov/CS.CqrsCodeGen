using WinForms.Launcher.Buisness.Common;

namespace WinForms.Launcher.Buisness.Models;

internal sealed class PropertyVm : ViewModelBase
{
    private string _typeAndName = string.Empty;

    private string _type = string.Empty;
    private string _name = string.Empty;

    public PropertyVm()
    {
    }

    public string TypeAndName
    {
        get => _typeAndName;
        set => SetWhenNotEqual(value);
    }

    public (string type, string name) ToPropertyDefinition() => (_type, _name);

    protected override void OnPropertyChanged(string pptName)
    {
        base.OnPropertyChanged(pptName);

        if (pptName == nameof(TypeAndName) && !string.IsNullOrEmpty(_typeAndName))
        {
            var split = _typeAndName.Split(" ");
            if (split.Length > 1)
            { 
                _type = split[0];
                _name = split[1];
                
            }
        }
    }
}
