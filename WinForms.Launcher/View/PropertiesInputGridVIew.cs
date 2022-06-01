using System.ComponentModel;
using System.Diagnostics;
using WinForms.Launcher.Buisness.DataBinding;
using WinForms.Launcher.Buisness.Models;

namespace WinForms.Launcher.View;

internal sealed partial class PropertiesInputGridView : UserControl
{
    public PropertiesInputGridView()
    {
        InitializeComponent();
        gridView.EditMode = DataGridViewEditMode.EditOnEnter ;
        gridView.AllowUserToAddRows = true;
        gridView.AllowUserToDeleteRows = true;
    }

    [Browsable(false)]
    public BindingList<PropertyVm> DataSource
    {
        set => gridView.DataSource = value;
    }

    private void gridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
    {
        DataGridViewTextBoxEditingControl te = (DataGridViewTextBoxEditingControl)e.Control;
        
        te.AutoCompleteMode = AutoCompleteMode.Suggest;
        te.AutoCompleteSource = AutoCompleteSource.CustomSource;
        te.AutoCompleteCustomSource.AddRange(TypesRepository.Types);
    }

    private void gridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
    {
        gridView.Columns[0].HeaderText = "Request data model (each line is a property declaration, e.g. int id)";
        gridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    }
}
