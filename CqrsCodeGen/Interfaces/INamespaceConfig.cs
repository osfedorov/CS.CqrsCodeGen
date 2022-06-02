namespace CqrsCodeGen.Interfaces;

public interface INamespaceConfig
{
    public string Base { get; set; }
    public string Business { get; set; }
    public string DataAcess { get; set; }
    public string Grpc { get; set; }
    public string Product { get; set; }
}
