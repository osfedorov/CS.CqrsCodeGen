using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WinForms.Launcher.View;

namespace WinForms.Launcher;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(ServiceProvider.GetRequiredService<MainForm>());
    }

    public static IServiceProvider ServiceProvider { get; } = CreateHostBuilder().Build().Services;

    private static IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) => {
                services.AddTransient<MainForm>();
                services.AddTransient<MainScreen>();
                services.AddTransient<InputCqrs>();
            });
    }
}