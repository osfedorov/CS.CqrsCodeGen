namespace CqrsCodeGen.Intrernals.Configuration;

internal sealed class DtoConfig : CodeGenConfigBase
{
    public string ClassSuffix { get; init; }

    public override string ClassName => BaseName + ClassSuffix;
}
