namespace TemplateMethodDemo.TemplateMethod.Gateways;

public interface ICadastroGateway
{
    Task<DadosPessoa?> ObterDadosPessoaAsync(Guid idPessoa);
    Task<DadosBancarios?> ObterDadosBancariosAsync(Guid idPessoa);
}