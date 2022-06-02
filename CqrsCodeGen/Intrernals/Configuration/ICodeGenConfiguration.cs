using CqrsCodeGen.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace CqrsCodeGen.Intrernals.Configuration;

internal interface ICodeGenConfiguration
{
    string OutputPath { get; }
    INamespaceConfig NamespaceConfig { get; }

    ActionConfig Action { get; }
    ActionResponseModelConfig ActionResponse { get; }
    ActionHandlerConfig ActionHandler { get; }

    GrpcServiceConfig GrpcService { get; }
    DtoConfig RequestDto { get; }
    DtoConfig ResponseDto { get; }
}
