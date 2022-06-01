namespace WinForms.Launcher.View;

public partial class InputCqrs : UserControl
{
    public InputCqrs()
    {
        InitializeComponent();

        gridMainSection.DataSource = configuration.MainSection;
        gridMainSection.EditMode = DataGridViewEditMode.EditOnEnter;

        requestPropertiesView.DataSource = configuration.RequestProperties;
    }

    private void gridMainSection_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
    {
        foreach (int row in configuration.GetMainSectionDelimiterIndices())
        {
            gridMainSection.Rows[row].DividerHeight = 2;
        }

        gridMainSection.Columns[gridMainSection.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    }
}
