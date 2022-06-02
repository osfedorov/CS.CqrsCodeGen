namespace CqrsCodeGen.Intrernals.Configuration;

public static class Extensions
{
    public static string AddNamespace(this string baseNamespace, string chunk) =>
        string.IsNullOrWhiteSpace(baseNamespace) ? chunk : $"{baseNamespace}.{chunk}";
}