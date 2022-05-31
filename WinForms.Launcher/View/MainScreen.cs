namespace WinForms.Launcher.View;

internal sealed partial class MainScreen : UserControl
{
    private MainScreen()
    {
        InitializeComponent();
    }

    public MainScreen(InputCqrs view) : this()
    {
        view.Dock = DockStyle.Fill;
        Controls.Add(view);
    }
}
