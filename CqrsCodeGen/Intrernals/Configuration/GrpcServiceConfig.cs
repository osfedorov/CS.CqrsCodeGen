namespace CqrsCodeGen.Intrernals.Configuration;

internal sealed class GrpcServiceConfig : CodeGenConfigBase
{
    public GrpcServiceConfig(string className)
    {
        ClassName = className;
    }

    public string ReturnGenericType { get; init; }
    public string ResponseDto { get; init; }
    public string Request { get; init; }
    public string Action { get; init; }
    public string ActionResponse { get; init; }

    public override string ClassName { get; }
}
