namespace TemplateMethodDemo.TemplateMethod;

public class DadosPessoa
{
    public Guid Id { get; set; } = Guid.Empty;
    public string NomeCompleto { get; set; } = string.Empty;
    public IEnumerable<DocumentoPessoa> Documentos { get; set; } = Array.Empty<DocumentoPessoa>();


    public class DocumentoPessoa
    {
        public string Numero { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
    }
}