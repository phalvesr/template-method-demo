using TemplateMethodDemo.TemplateMethod.Data.Database;
using TemplateMethodDemo.TemplateMethod.Gateways;
using TemplateMethodDemo.Utils;

namespace TemplateMethodDemo.TemplateMethod.Pattern;

public class InclusaoCadastral : CadastroClienteBase
{
    private readonly IImportaCadastroPessoaGateway _importaCadastroPessoaGateway;
    private readonly IDateTimeProvider _dateTimeProvider;

    public InclusaoCadastral(
        ICadastroGateway cadastroGateway, 
        IImportaCadastroPessoaGateway importaCadastroPessoaGateway, 
        IDateTimeProvider dateTimeProvider) : base(cadastroGateway)
    {
        _importaCadastroPessoaGateway = importaCadastroPessoaGateway;
        _dateTimeProvider = dateTimeProvider;
    }

    protected override DadosPessoaPersistenceModel OnMapeamentoPessoaAsync(DadosPessoa dadosPessoa)
    {
        return new DadosPessoaPersistenceModel
        {
            Id = dadosPessoa.Id,
            NomeCompleto = dadosPessoa.NomeCompleto,
            NomeSocial = string.Empty,
            Documentos = dadosPessoa.Documentos.Select(x => new DadosPessoaPersistenceModel.DocumentoPessoa
            {
                Numero = x.Numero,
                Tipo = x.Tipo
            }).ToArray(),
            DataHoraAtualizacao = null,
            DataHoraInclusao = _dateTimeProvider.UtcNow
        };
    }

    protected override DadosBancariosPersistenceModel OnMapeamentoBancoAsync(DadosBancarios dadosBancarios)
    {
        return new DadosBancariosPersistenceModel
        {
            Id = dadosBancarios.Id,
            IdPessoa = dadosBancarios.IdPessoa,
            Agencia = dadosBancarios.Agencia,
            Conta = dadosBancarios.Conta,
            Digito = dadosBancarios.Digito,
            DataHoraAtualizacao = null,
            DataHoraInsersao = _dateTimeProvider.UtcNow
        };
    }

    protected override Task OnPersistirDadosAsync(DadosPessoaPersistenceModel novosDadosPessoa,
        DadosBancariosPersistenceModel novosDadosBancario)
    {
        _importaCadastroPessoaGateway.CriarNovoCadastro(novosDadosPessoa, novosDadosBancario);
        
        return Task.CompletedTask;
    }
}