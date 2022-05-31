using CqrsCodeGen.Interfaces;
using WinForms.Launcher.Buisness.Common;

namespace WinForms.Launcher.Buisness.Models;

internal class ActionTypeVm : NameValue
{
    private ActionType _actionType;

    public ActionTypeVm(string name, string value = "") : base(name, value)
    {
    }

    public ActionType ActionType
    {
        get => _actionType;
        set
        { 
            _actionType = value;
            Value = _actionType.ToString().ToLower();
        }
    }
}

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
