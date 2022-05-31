namespace WinForms.Launcher.View;

public partial class InputCqrs : UserControl
{
    public InputCqrs()
    {
        InitializeComponent();

        gridMainSection.DataSource = configuration.MainSection;
        gridDtoSection.DataSource = configuration.DtoSection;
    }

    private void gridMainSection_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
    {
        DataGridViewTextBoxEditingControl te =
(DataGridViewTextBoxEditingControl)e.Control;
        te.AutoCompleteMode = AutoCompleteMode.Append;
        te.AutoCompleteSource = AutoCompleteSource.CustomSource;
        te.AutoCompleteCustomSource.AddRange(new string[] {"one", "two",
"three"});
    }
}
