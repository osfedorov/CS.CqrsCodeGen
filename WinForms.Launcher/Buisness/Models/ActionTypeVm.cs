using CqrsCodeGen.Interfaces;

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

    protected override void OnPropertyChanged(string pptName)
    {
        base.OnPropertyChanged(pptName);

        if (pptName == nameof(Value) && !string.IsNullOrEmpty(Value))
        {
            _actionType = char.ToLower(Value[0]) switch
            {
                'c' => ActionType.Command,
                'q' => ActionType.Query,
                _ => ActionType.Command,
            };

            Value = _actionType.ToString().ToLower();
        }
    }
}
