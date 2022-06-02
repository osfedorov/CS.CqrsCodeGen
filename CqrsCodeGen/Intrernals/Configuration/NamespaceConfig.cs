using CqrsCodeGen.Interfaces;
using System;
using System.Diagnostics.CodeAnalysis;

namespace CqrsCodeGen.Intrernals.Configuration;

internal class NamespaceConfig : INamespaceConfig
{
    private const string VsSolutionSuffix = ".sln";
    private const string VsProjectSuffix = ".csproj";

    public string Base { get; set; } = string.Empty;
    public string Business { get; set; } = string.Empty;
    public string Grpc { get; set; } = string.Empty;
    public string Product { get; set; } = string.Empty;
    public string DataAcess { get; set; } = string.Empty;

    public static NamespaceConfig Create([NotNull] ICodeGenConfigurationSource cfg)
    {
        return string.IsNullOrWhiteSpace(cfg.TargetLocation)
            ? InitFromConfig(cfg)
            : InitFromTargetDirectoryStructure(cfg);
    }

    private NamespaceConfig()
    {
    }

    private static NamespaceConfig InitFromTargetDirectoryStructure(ICodeGenConfigurationSource cfg)
    {
        if (cfg.TargetLocation is null)
        {
            throw new ArgumentException("Target location is not set", nameof(cfg));
        }

        string targetLocation = cfg.TargetLocation;
        if (!Directory.Exists(targetLocation))
        {
            throw new InvalidOperationException($"{targetLocation} doesn't exist");
        }

        // find the directory that contains VS solution.
        // This action is unneccessary 
        var vsSolutionDirs = FindDirContainingFile(targetLocation, VsSolutionSuffix);
        string pathFromSlnToTarget = string.Join("\\", vsSolutionDirs);

        if (vsSolutionDirs.Count < 2)
        {
            throw new ApplicationException($"{pathFromSlnToTarget} have no VS solution");
        }

        // find the directory containing VS project
        var vsTargetProjectDirs = FindDirContainingFile(targetLocation, VsProjectSuffix);

        if (vsSolutionDirs.Count < 1)
        {
            throw new ApplicationException($"{pathFromSlnToTarget} have no VS project");
        }

        string baseNsp = string.Join('.', vsTargetProjectDirs[0].Split('.').Take(2));
        string businessProj = vsTargetProjectDirs[0];

        return new NamespaceConfig
        {
            Base = businessProj,
            Business = CreateBusinessNamespace(cfg, string.Join('.', vsTargetProjectDirs)),
            Grpc = baseNsp.AddNamespace("GrpcClient"),
            DataAcess = baseNsp.AddNamespace("DataAccess"),
            Product = baseNsp.Split('.').Last()
        };
    }

    private static IReadOnlyList<string> FindDirContainingFile(string targetLocation, string fileSuffix)
    {
        var directories = targetLocation.Split("\\");
        var dirInfo = new DirectoryInfo(targetLocation);
        int index = directories.Length - 1;

        while (0 != index && null != dirInfo)
        {
            if (dirInfo.EnumerateFiles().Any(f => f.Name.EndsWith(fileSuffix)))
            {
                return directories.TakeLast(directories.Length - index).ToList();
            }

            dirInfo = dirInfo.Parent;
            --index;
        }

        throw new ApplicationException($"Failed to find directory with VS solution file. Upwards search was started at {targetLocation}");
    }

    private static NamespaceConfig InitFromConfig(ICodeGenConfigurationSource cfg)
    {
        var baseNsp = $"CS.{cfg.Project}";
        var product = string.IsNullOrWhiteSpace(cfg.Project)
            ? baseNsp.Split('.').Take(2).Last()
            : cfg.Project;

        return new NamespaceConfig
        {
            Base = baseNsp,
            Business = CreateBusinessNamespace(cfg, baseNsp.AddNamespace(cfg.BusinessNamespace)),
            Grpc = baseNsp.AddNamespace(cfg.DtoNamespace),
            DataAcess = baseNsp.AddNamespace("DataAccess"),
            Product = product
        };
    }

    private static string CreateBusinessNamespace(ICodeGenConfigurationSource cfg, string businessNamespace)
    {
        string actionNspaceChunk = cfg.Type switch
        {
            ActionType.Command => "Commands",
            ActionType.Query => "Queries",
            _ => throw new NotSupportedException($"{cfg.Type} is not supported")
        };

        return businessNamespace
            .AddNamespace(actionNspaceChunk)
            .AddNamespace(cfg.MethodName);
    }
}
