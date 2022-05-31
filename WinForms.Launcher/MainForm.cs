using WinForms.Launcher.View;

namespace WinForms.Launcher;

internal sealed partial class MainForm : Form
{
    private MainForm()
    {
        InitializeComponent();
    }

    public MainForm(MainScreen mainScreen) : this()
    {
        mainScreen.Dock = DockStyle.Fill;
        Controls.Add(mainScreen);
    }
}
