using Console.Launcher;
using CqrsCodeGen;
using CqrsCodeGen.CodeGeneration;
using CqrsCodeGen.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Setup Host
var host = CreateDefaultBuilder().Build();

// Invoke Worker
using IServiceScope serviceScope = host.Services.CreateScope();
IServiceProvider provider = serviceScope.ServiceProvider;
var codeGeneraot = provider.GetRequiredService<ICodeGenerator>();
await codeGeneraot.GenerateAsync();

host.Run();

static IHostBuilder CreateDefaultBuilder()
{
    return Host.CreateDefaultBuilder()
        .ConfigureAppConfiguration(app =>
        {
            app.AddJsonFile("appsettings.json");
        })
        .ConfigureServices(services =>
        {
            services.AddSingleton<ICodeGenConfigurationSource, CodeGenConfigSource>();
            services.AddCodeGenerator();
        });
}