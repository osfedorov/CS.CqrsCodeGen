using CqrsCodeGen.Interfaces;
using CqrsCodeGen.Intrernals.DataModels;
using System.Diagnostics.CodeAnalysis;

namespace CqrsCodeGen.Intrernals.Configuration;

internal class ConfigBuilder : ICodeGenConfiguration
{
    public string BaseNamespace { get; init; }
    public string OutputPath { get; init; }

    public ActionConfig Action { get; init; }
    public ActionResponseModelConfig ActionResponse { get; init; }
    public ActionHandlerConfig ActionHandler { get; init; }

    [MaybeNull]
    public string GrpcServiceName { get; init; }

    public GrpcServiceConfig GrpcService { get; init; }
    public DtoConfig RequestDto { get; init; }
    public DtoConfig ResponseDto { get; init; }

    [NotNull]
    private readonly ICodeGenConfigurationSource _configuration;

    public ConfigBuilder(ICodeGenConfigurationSource configuration)
    {
        if (configuration is null)
        {
            throw new ArgumentException(nameof(configuration), "configuration is not set");
        }

        _configuration = configuration;

        OutputPath = _configuration.OutputPath;
        GrpcServiceName = _configuration.GrpcServiceName;
        BaseNamespace = $"CS.{_configuration.Project}";

        RequestDto = CreateDtoConfig(DtoType.Request);
        ResponseDto = CreateDtoConfig(DtoType.Response);

        ActionResponse = CreateActionResponseConfig();

        string resultType = _configuration.ResponseModelBaseName == "void"
            ? _configuration.ResponseGenericType.ToString()
            : $"{_configuration.ResponseGenericType}<{ActionResponse.ClassName}>";

        Action = CreateActionConfig(resultType);
        ActionHandler = new ActionHandlerConfig
        {
            BaseName = _configuration.MethodName,
            BaseNamespace = BaseNamespace,
            Namespace = CreateBusinessNamespace(string.Empty),
            Project = _configuration.Project,

            ResponseType = resultType,
            Command = Action.ClassName,
        };

        GrpcService = new GrpcServiceConfig(_configuration.GrpcServiceName)
        {
            BaseName = _configuration.MethodName,
            BaseNamespace = RequestDto.BaseNamespace,
            Namespace = BaseNamespace,

            ReturnGenericType = $"{_configuration.ResponseGenericType}",
            ResponseDto = ResponseDto.ClassName,
            Request = RequestDto.ClassName,
            Action = Action.ClassName,
            ActionResponse = ActionResponse.ClassName,
        };
    }

    private ActionConfig CreateActionConfig(string responseType)
    {
        return new ActionConfig
        {
            BaseName = _configuration.MethodName,
            BaseNamespace = BaseNamespace,
            ResponseType = responseType,
            ActionType = _configuration.Type.ToString(),
            Namespace = CreateBusinessNamespace(string.Empty),
            Properties = _configuration.RequestProperties.Select(kvp => new Property { Type = kvp.type, Name = kvp.name }).ToList()
        };
    }

    private ActionResponseModelConfig CreateActionResponseConfig()
    {
        return new ActionResponseModelConfig
        {
            BaseName = _configuration.MethodName,
            BaseNamespace = BaseNamespace,
            IsExistingType = _configuration.IsResponseModelExistingType,
            Namespace = CreateBusinessNamespace("Models"),
            Name = _configuration.ResponseModelBaseName,
            Properties = _configuration.ResponseModelProperties.Select(kvp => new Property { Type = kvp.type, Name = kvp.name }).ToList()
        };
    }

    private DtoConfig CreateDtoConfig(DtoType type)
    {
        var ppts = type switch
        {
            DtoType.Request => _configuration.RequestProperties,
            DtoType.Response => _configuration.ResponseDtoProperties,
            _ => throw new NotSupportedException($"{type} is not supported")
        };

        string baseNamespace = string.Join('.', BaseNamespace.Split('.').Take(2)).AddNamespace(_configuration.DtoNamespace);

        return new DtoConfig
        {
            BaseName = _configuration.MethodName,
            BaseNamespace = baseNamespace,
            Namespace = baseNamespace.AddNamespace(type.ToString()),
            ClassSuffix = type.ToString(),
            Properties = ppts.Select(kvp => new Property { Type = kvp.type, Name = kvp.name }).ToList()
        };
    }

    private string CreateBusinessNamespace(string middleName)
    {
        string actionNspaceChunk = string.IsNullOrEmpty(middleName) ?
            _configuration.Type switch
            {
                ActionType.Command => "Commands",
                ActionType.Query => "Queries",
                _ => throw new NotSupportedException($"{_configuration.Type} is not supported")
            }
            : middleName;

        return BaseNamespace
            .AddNamespace(_configuration.BusinessNamespace)
            .AddNamespace(actionNspaceChunk)
            .AddNamespace(_configuration.MethodName);
    }
}

public static class Extensions
{
    public static string AddNamespace(this string baseNamespace, string chunk) =>
        string.IsNullOrWhiteSpace(baseNamespace) ? chunk : $"{baseNamespace}.{chunk}";
}