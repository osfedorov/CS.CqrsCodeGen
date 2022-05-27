using System.Diagnostics.CodeAnalysis;

namespace CqrsCodeGen.Intrernals.Configuration;

internal interface ICodeGenConfiguration
{
    /// <summary>
    ///  The start portion of the namespace: CS.{Project}
    /// </summary>
    string BaseNamespace { get; init; }
    string OutputPath { get; init; }

    ActionConfig Action { get; init; }
    ActionResponseModelConfig ActionResponse { get; init; }
    ActionHandlerConfig ActionHandler { get; init; }

    [MaybeNull]
    string GrpcServiceName { get; init; }

    GrpcServiceConfig GrpcService { get; init; }
    DtoConfig RequestDto { get; init; }
    DtoConfig ResponseDto { get; init; }
}
