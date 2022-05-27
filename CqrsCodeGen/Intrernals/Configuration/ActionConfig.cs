namespace CqrsCodeGen.Intrernals.Configuration;

internal sealed class ActionConfig : CodeGenConfigBase
{
    public string ActionType { get; init; }
    public string ResponseType { get; init; }

    public override string ClassName => BaseName + ActionType;
}
