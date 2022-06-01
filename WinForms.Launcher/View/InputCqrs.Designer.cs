namespace WinForms.Launcher.View;

partial class InputCqrs
{
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            this.layoutMain = new System.Windows.Forms.TableLayoutPanel();
            this.gridMainSection = new System.Windows.Forms.DataGridView();
            this.requestPropertiesView = new WinForms.Launcher.View.PropertiesInputGridView();
            this.configuration = new WinForms.Launcher.Buisness.DataBinding.Configuration(this.components);
            this.layoutMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMainSection)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutMain
            // 
            this.layoutMain.ColumnCount = 1;
            this.layoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutMain.Controls.Add(this.gridMainSection, 0, 0);
            this.layoutMain.Controls.Add(this.requestPropertiesView, 0, 1);
            this.layoutMain.Location = new System.Drawing.Point(3, 3);
            this.layoutMain.Name = "layoutMain";
            this.layoutMain.RowCount = 2;
            this.layoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutMain.Size = new System.Drawing.Size(608, 399);
            this.layoutMain.TabIndex = 0;
            // 
            // gridMainSection
            // 
            this.gridMainSection.AllowUserToAddRows = false;
            this.gridMainSection.AllowUserToDeleteRows = false;
            this.gridMainSection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMainSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMainSection.Location = new System.Drawing.Point(3, 3);
            this.gridMainSection.Name = "gridMainSection";
            this.gridMainSection.RowTemplate.Height = 25;
            this.gridMainSection.Size = new System.Drawing.Size(602, 193);
            this.gridMainSection.TabIndex = 0;
            this.gridMainSection.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.gridMainSection_DataBindingComplete);
            // 
            // requestPropertiesView
            // 
            this.requestPropertiesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.requestPropertiesView.Location = new System.Drawing.Point(3, 202);
            this.requestPropertiesView.Name = "requestPropertiesView";
            this.requestPropertiesView.Size = new System.Drawing.Size(602, 194);
            this.requestPropertiesView.TabIndex = 1;
            // 
            // InputCqrs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutMain);
            this.Name = "InputCqrs";
            this.Size = new System.Drawing.Size(951, 577);
            this.layoutMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridMainSection)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private TableLayoutPanel layoutMain;
    private Buisness.DataBinding.Configuration configuration;
    private DataGridView gridMainSection;
    private PropertiesInputGridView requestPropertiesView;
}
