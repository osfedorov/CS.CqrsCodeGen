using System.Diagnostics.CodeAnalysis;

namespace CqrsCodeGen.Interfaces;

public interface ICodeGenConfigurationSource
{
    [NotNull] string OutputPath => "Generated";

    [NotNull] string Project { get; }
    [MaybeNull] string GrpcServiceName { get; }
    [NotNull] string MethodName { get; }

    ActionType Type { get; }

    /// <summary>
    /// gRpc client namespace relative to CS, e.g. "GrpcClient".
    /// </summary>
    [NotNull] string DtoNamespace => "GrpcClient";

    /// <summary>
    /// A namespace chunk relative to CS.{Project}, e.g. "Contracts" in CS.Business.Contracts.
    /// </summary>
    [NotNull] string BusinessNamespace => "Business";

    [NotNull] List<(string type, string name)> RequestProperties { get; }
    [NotNull] List<(string type, string name)> ResponseDtoProperties { get; }

    ResponseGenericType ResponseGenericType => ResponseGenericType.Result;
    bool IsResponseModelExistingType { get; }
    [NotNull] string ResponseModelBaseName => $"{MethodName}Dto"; // response model class base name
    [NotNull] List<(string type, string name)> ResponseModelProperties { get; }
}
