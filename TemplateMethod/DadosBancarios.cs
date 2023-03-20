namespace TemplateMethodDemo.TemplateMethod;

public class DadosBancarios
{
    public Guid IdPessoa { get; set; }
    public Guid Id { get; set; }
    public string Agencia { get; set; } = string.Empty;
    public string Conta { get; set; } = string.Empty;
    public int Digito { get; set; }
}