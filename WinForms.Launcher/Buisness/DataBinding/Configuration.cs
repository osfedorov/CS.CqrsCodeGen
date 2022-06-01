using System.ComponentModel;
using WinForms.Launcher.Buisness.Models;

namespace WinForms.Launcher.Buisness.DataBinding;

internal sealed partial class Configuration : Component
{
    private static readonly NameValue _business = new NameValue("Business namespace");
    private static readonly NameValue _dto = new NameValue("Dto namespace");

    public BindingList<NameValue> MainSection { get; } = new()
    {
        new NameValue("Method name"),
        new ActionTypeVm("Type", "command"),
        new NameValue("Project"),
        
        _business,

        _dto,
        new NameValue("Service name"),
    };

    private IEnumerable<NameValue> _delimiters = new[] { _business, _dto};

    public BindingList<PropertyVm> RequestProperties { get; } = new()
    {
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

    public IReadOnlyCollection<int> GetMainSectionDelimiterIndices()
    {
        return _delimiters.Select(row =>
            {
                int index = MainSection.IndexOf(row) - 1;
                return index < 0 ? 0 : index;
            })
            .ToList();
    }
}
