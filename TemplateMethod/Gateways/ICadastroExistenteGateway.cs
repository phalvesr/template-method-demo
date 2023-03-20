using TemplateMethodDemo.TemplateMethod.Data.Database;

namespace TemplateMethodDemo.TemplateMethod.Gateways;

public interface ICadastroExistenteGateway
{
    DadosPessoaPersistenceModel? ObterDadosPessoa(Guid idPessoa);
    DadosBancariosPersistenceModel? ObterDadosBancariosPorPessoa(Guid idPessoa);
}