using CqrsCodeGen.CodeGeneration;
using CqrsCodeGen.Intrernals.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CqrsCodeGen;

public static class DependencyInjection
{
    public static IServiceCollection AddCodeGenerator(this IServiceCollection services)
    {
        services.AddSingleton<ICodeGenConfiguration, ConfigBuilder>();
        services.AddSingleton<ICodeGenerator, CodeGenerator>();

        return services;
    }
}
