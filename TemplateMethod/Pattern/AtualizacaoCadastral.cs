using TemplateMethodDemo.TemplateMethod.Data.Database;
using TemplateMethodDemo.TemplateMethod.Gateways;
using TemplateMethodDemo.Utils;

namespace TemplateMethodDemo.TemplateMethod.Pattern;

public class AtualizacaoCadastral : CadastroClienteBase
{
    private readonly ICadastroGateway _cadastroGateway;
    private readonly IImportaCadastroPessoaGateway _importaCadastroPessoaGateway;
    private readonly ICadastroExistenteGateway _cadastroExistenteGateway;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AtualizacaoCadastral(
        ICadastroGateway cadastroGateway, 
        IImportaCadastroPessoaGateway importaCadastroPessoaGateway, 
        ICadastroExistenteGateway cadastroExistenteGateway, 
        IDateTimeProvider dateTimeProvider) : base(cadastroGateway)
    {
        _cadastroGateway = cadastroGateway;
        _importaCadastroPessoaGateway = importaCadastroPessoaGateway;
        _cadastroExistenteGateway = cadastroExistenteGateway;
        _dateTimeProvider = dateTimeProvider;
    }

    protected override DadosPessoaPersistenceModel OnMapeamentoPessoaAsync(DadosPessoa dadosPessoa)
    {
        var situacaoPessoaAnterior = _cadastroExistenteGateway.ObterDadosPessoa(dadosPessoa.Id);
        
        return new DadosPessoaPersistenceModel
        {
            Id = dadosPessoa.Id,
            NomeCompleto = dadosPessoa.NomeCompleto,
            Documentos = dadosPessoa.Documentos.Select(x => new DadosPessoaPersistenceModel.DocumentoPessoa
            {
                Numero = x.Numero,
                Tipo = x.Tipo
            }).ToArray(),
            NomeSocial = situacaoPessoaAnterior?.NomeSocial ?? string.Empty,
            DataHoraInclusao = situacaoPessoaAnterior!.DataHoraInclusao,
            DataHoraAtualizacao = _dateTimeProvider.UtcNow
        };
    }

    protected override DadosBancariosPersistenceModel OnMapeamentoBancoAsync(DadosBancarios dadosBancarios)
    {
        var situacaoBancoAnterior = _cadastroExistenteGateway.ObterDadosBancariosPorPessoa(dadosBancarios.IdPessoa);
        
        return new DadosBancariosPersistenceModel
        {
            Agencia = dadosBancarios.Agencia,
            Conta = dadosBancarios.Conta,
            Digito = dadosBancarios.Digito,
            IdPessoa = dadosBancarios.IdPessoa,
            Id = dadosBancarios.Id,
            DataHoraAtualizacao = _dateTimeProvider.UtcNow,
            DataHoraInsersao = situacaoBancoAnterior!.DataHoraInsersao
        };
    }

    protected override Task OnPersistirDadosAsync(DadosPessoaPersistenceModel novosDadosPessoa,
        DadosBancariosPersistenceModel novosDadosBancario)
    {
        _importaCadastroPessoaGateway.AtualizaDadosCadastrais(novosDadosPessoa, novosDadosBancario);
        
        return Task.CompletedTask;
    }
}