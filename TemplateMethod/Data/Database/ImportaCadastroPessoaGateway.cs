using TemplateMethodDemo.TemplateMethod.Gateways;

namespace TemplateMethodDemo.TemplateMethod.Data.Database;

public class ImportaCadastroPessoaGateway : IImportaCadastroPessoaGateway, ICadastroExistenteGateway

{
    private Dictionary<Guid, DadosPessoaPersistenceModel> _pessoas = new()
    {
        { Guid.Parse("00000000-0000-0000-0000-000000000001"), Mock.PessoaId1 }
    };

    private Dictionary<Guid, DadosBancariosPersistenceModel> _bancosPessoa = new()
    {
        { Guid.Parse("00000000-0000-0000-0000-000000000001"), Mock.BancoPessoa1 }
    };

    public bool AtualizaDadosCadastrais(DadosPessoaPersistenceModel pessoa, DadosBancariosPersistenceModel banco)
    {
        if (!_pessoas.TryGetValue(pessoa.Id, out _) || !_bancosPessoa.TryGetValue(banco.IdPessoa, out _))
        {
            return false;
        }

        _pessoas.Remove(pessoa.Id);
        _bancosPessoa.Remove(pessoa.Id);
        
        _pessoas[pessoa.Id] = pessoa;
        _bancosPessoa[pessoa.Id] = banco;

        return true;
    }

    public bool CriarNovoCadastro(DadosPessoaPersistenceModel pessoa, DadosBancariosPersistenceModel banco)
    {
        try
        {
            _pessoas.Add(pessoa.Id, pessoa);
            _bancosPessoa.Add(banco.Id, banco);

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public IReadOnlyDictionary<Guid, DadosPessoaPersistenceModel> GetPessoas()
    {
        return _pessoas;
    }
    
    public IReadOnlyDictionary<Guid, DadosBancariosPersistenceModel> GetBancos()
    {
        return _bancosPessoa;
    }

    public DadosPessoaPersistenceModel? ObterDadosPessoa(Guid idPessoa)
    {
        return _pessoas.TryGetValue(idPessoa, out var pessoa) ? pessoa : null;
    }
    
    public DadosBancariosPersistenceModel? ObterDadosBancariosPorPessoa(Guid idPessoa)
    {
        return _bancosPessoa.TryGetValue(idPessoa, out var banco) ? banco : null;
    }
}

public class DadosPessoaPersistenceModel
{
    public Guid Id { get; init; } = Guid.Empty;
    
    public string NomeCompleto { get; init; } = string.Empty;
    
    public string NomeSocial { get; init; } = string.Empty;
    
    public IReadOnlyList<DocumentoPessoa> Documentos { get; init; } = Array.Empty<DocumentoPessoa>();
    
    public DateTime DataHoraInclusao { get; init; }
    
    public DateTime? DataHoraAtualizacao { get; init; }
    
    public class DocumentoPessoa
    {
        public string Numero { get; init; } = String.Empty;
        
        public string Tipo { get; init; } = String.Empty;
    }
}

public class DadosBancariosPersistenceModel
{
    public Guid IdPessoa { get; init; }
    
    public Guid Id { get; init; }
    
    public string Agencia { get; init; } = string.Empty;
    
    public string Conta { get; init; } = string.Empty;
    
    public int Digito { get; init; }
    
    public DateTime DataHoraInsersao { get; init; }
    
    public DateTime? DataHoraAtualizacao { get; init; }
}

