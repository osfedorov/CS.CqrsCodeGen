using CqrsCodeGen.Intrernals.DataModels;
using System.Diagnostics.CodeAnalysis;

namespace CqrsCodeGen.Intrernals.Configuration;

internal abstract class CodeGenConfigBase
{
    public string BaseName { get; init; }
    public string Namespace { get; init; }
    public string BaseNamespace { get; init; } // CS.{Project}

    public abstract string ClassName { get; }
    public IReadOnlyList<Property> Properties { get; init; } = new List<Property>();
}
