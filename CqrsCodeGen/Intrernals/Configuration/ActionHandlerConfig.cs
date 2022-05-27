namespace CqrsCodeGen.Intrernals.Configuration;

internal sealed class ActionHandlerConfig : CodeGenConfigBase
{
    public string Command { get; init; }
    public string ResponseType { get; init; }
    public string Project { get; init; }

    public override string ClassName => $"{Command}Handler";
}
