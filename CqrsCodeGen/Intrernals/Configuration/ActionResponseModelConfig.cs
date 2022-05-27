namespace CqrsCodeGen.Intrernals.Configuration;

internal sealed class ActionResponseModelConfig : CodeGenConfigBase
{
    public bool IsExistingType { get; init; }
    public string Name { get; init; }
    public override string ClassName => IsExistingType
        ? BaseName
        : BaseName + (!string.IsNullOrEmpty(Name) ? Name : "Model");
}
