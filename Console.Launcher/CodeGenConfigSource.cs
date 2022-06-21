using CqrsCodeGen.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Console.Launcher;

internal class CodeGenConfigSource : ICodeGenConfigurationSource
{
    public string OutputPath { get; }
    public string TargetLocation { get; }
    public string Project { get; }
    public string GrpcServiceName { get; }
    public string MethodName { get; }
    public ActionType Type { get; }
    public string DtoNamespace { get; }
    public string BusinessNamespace { get; }
    public List<(string type, string name)> RequestProperties { get; } = new();
    public List<(string type, string name)> ResponseDtoProperties { get; } = new();
    public ResponseGenericType ResponseGenericType { get; }
    public string ResponseModelBaseName { get; }
    public bool IsResponseModelExistingType { get; }
    public List<(string type, string name)> ResponseModelProperties { get; } = new();


    public CodeGenConfigSource(IConfiguration configuration)
    {
        OutputPath = configuration["Output:Path"];
        TargetLocation = configuration["Base:TargetLocation"];
        MethodName = configuration["Base:MethodName"];
        Project = configuration["Base:Project"];
        GrpcServiceName = configuration["Base:Service"];

        Type = configuration["Base:Type"].ToLower() switch
        {
            "command" => ActionType.Command,
            "query" => ActionType.Query,
            _ => throw new NotSupportedException($"{configuration["Base:Type"]} is not supported")
        };

        DtoNamespace = configuration["Dto:Namespace"];
        BusinessNamespace = configuration["Business:Namespace"];
        ResponseModelBaseName = configuration["ResponseModel:Name"];
        ResponseGenericType = configuration["Action:ResultGenericType"].ToLower() switch
        {
            "result" => ResponseGenericType.Result,
            "list" or "resultlist" => ResponseGenericType.ResultList,
            "page" or "pagedresult" => ResponseGenericType.PagedResult,
            _ => throw new NotSupportedException($"Generic result type \"{configuration["Action:ResultGenericType"]}\" not supported")
        };

        IsResponseModelExistingType = configuration["ResponseModel:ExistingType"]?.ToLower() switch
        {
            "true" or "yes" => true,
            _ => false
        };

        RequestProperties = ParseConfig(configuration, "Request:Properties");
        ResponseDtoProperties = ParseConfig(configuration, "Response:Properties");
        ResponseModelProperties = ParseConfig(configuration, "ResponseModel:Properties");
    }

    private List<(string type, string name)> ParseConfig(IConfiguration cfg, string sectionPath)
    {
        var section = cfg.GetSection(sectionPath);
        return section.AsEnumerable()
            .Where(kvp => !string.IsNullOrEmpty(kvp.Key) && !string.IsNullOrEmpty(kvp.Value) && kvp.Key != sectionPath)
            .Select(kvp => (kvp.Value, kvp.Key.Substring(sectionPath.Length + 1)))
            .Reverse()
            .ToList();
    }
}