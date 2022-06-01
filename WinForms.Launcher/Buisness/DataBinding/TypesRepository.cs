using System.ComponentModel;

namespace WinForms.Launcher.Buisness.DataBinding;

internal sealed class TypesRepository
{
    public static string[] Types => new string[] {"int", "string", "decimal", "List<", "List<int>" };
}
