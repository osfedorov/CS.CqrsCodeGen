using System.ComponentModel;
using WinForms.Launcher.Buisness.Models;

namespace WinForms.Launcher.Buisness.DataBinding;

internal sealed partial class Configuration : Component
{
    public BindingList<NameValue> MainSection { get; } = new()
    {
        new NameValue("Method name"),
        new ActionTypeVm("Type", "command"),
        new NameValue("Project")
    };

    public BindingList<NameValue> DtoSection { get; } = new()
    {
        new NameValue("Service name"),
        new NameValue("Dto namespace"),
    };

    public Configuration()
    {
        InitializeComponent();
    }

    public Configuration(IContainer container)
    {
        container.Add(this);

        InitializeComponent();
    }
}
