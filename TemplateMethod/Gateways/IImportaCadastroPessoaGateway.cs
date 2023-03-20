using TemplateMethodDemo.TemplateMethod.Data.Database;

namespace TemplateMethodDemo.TemplateMethod.Gateways;

public interface IImportaCadastroPessoaGateway
{
    public bool AtualizaDadosCadastrais(DadosPessoaPersistenceModel pessoa, DadosBancariosPersistenceModel banco);
    public bool CriarNovoCadastro(DadosPessoaPersistenceModel pessoa, DadosBancariosPersistenceModel banco);
}