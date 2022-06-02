using CqrsCodeGen.Interfaces;
using CqrsCodeGen.Intrernals.DataModels;
using System.Diagnostics.CodeAnalysis;

namespace CqrsCodeGen.Intrernals.Configuration;

internal class ConfigBuilder : ICodeGenConfiguration
{
    public string OutputPath { get; init; }
    public INamespaceConfig NamespaceConfig { get; }

    public ActionConfig Action { get; init; }
    public ActionResponseModelConfig ActionResponse { get; init; }
    public ActionHandlerConfig ActionHandler { get; init; }

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
        NamespaceConfig = Configuration.NamespaceConfig.Create(configuration);
        
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
            BaseNamespace = NamespaceConfig.Base,
            Namespace = NamespaceConfig.Business,
            DataAccess = NamespaceConfig.DataAcess,

            ResponseType = resultType,
            Command = Action.ClassName,
        };

        GrpcService = new GrpcServiceConfig(_configuration.GrpcServiceName)
        {
            BaseName = _configuration.MethodName,
            BaseNamespace = RequestDto.BaseNamespace,
            Namespace = NamespaceConfig.Base,

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
            BaseNamespace = NamespaceConfig.Base,
            ResponseType = responseType,
            ActionType = _configuration.Type.ToString(),
            Namespace = NamespaceConfig.Business,
            Properties = _configuration.RequestProperties.Select(kvp => new Property { Type = kvp.type, Name = kvp.name }).ToList()
        };
    }

    private ActionResponseModelConfig CreateActionResponseConfig()
    {
        return new ActionResponseModelConfig
        {
            BaseName = _configuration.MethodName,
            BaseNamespace = NamespaceConfig.Base,
            IsExistingType = _configuration.IsResponseModelExistingType,
            Namespace = NamespaceConfig.Business,
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

        return new DtoConfig
        {
            BaseName = _configuration.MethodName,
            BaseNamespace = NamespaceConfig.Grpc,
            Namespace = NamespaceConfig.Grpc.AddNamespace(type.ToString()),
            ClassSuffix = type.ToString(),
            Properties = ppts.Select(kvp => new Property { Type = kvp.type, Name = kvp.name }).ToList()
        };
    }
}
