using Bogus;
using TemplateMethodDemo.TemplateMethod;
using TemplateMethodDemo.TemplateMethod.Data.Database;

namespace TemplateMethodDemo;

public static class Mock
{
    private static Faker<DadosPessoaPersistenceModel> _dadosPessoaFaker = new();
    private static Faker<DadosBancariosPersistenceModel> _dadosBancarios = new();

    private static Faker<DadosPessoaPersistenceModel.DocumentoPessoa> _dadosDocumentoFaker = new Faker<DadosPessoaPersistenceModel.DocumentoPessoa>()
        .RuleFor(x => x.Numero, f => "82276713062")
        .RuleFor(x => x.Tipo, "CPF");
    
    internal static DadosPessoaPersistenceModel PessoaId1 => _dadosPessoaFaker
        .RuleFor(x => x.Id, Guid.Parse("00000000-0000-0000-0000-000000000001"))
        .RuleFor(x => x.NomeCompleto, "SENOR ABRAVANEL")
        .RuleFor(x => x.NomeSocial, "SILVIO SANTOS")
        .RuleFor(x => x.DataHoraInclusao, DateTime.Parse("2000-01-01T12:33:00.0000000+00:00"))
        .RuleFor(x => x.DataHoraAtualizacao, f => null)
        .Generate();
    
    internal static DadosPessoa PessoaId1Web => new Faker<DadosPessoa>()
        .RuleFor(x => x.Id, Guid.Parse("00000000-0000-0000-0000-000000000001"))
        .RuleFor(x => x.NomeCompleto, "SENOR ABRAVANEL")
        .Generate();

    internal static DadosBancariosPersistenceModel BancoPessoa1 => _dadosBancarios
        .RuleFor(x => x.IdPessoa, Guid.Parse("00000000-0000-0000-0000-000000000001"))
        .RuleFor(x => x.Agencia, "6611")
        .RuleFor(x => x.Conta, "45585")
        .RuleFor(x => x.DataHoraInsersao, DateTime.Parse("2000-01-01T12:33:00.0000000+00:00"))
        .RuleFor(x => x.DataHoraAtualizacao, f => null)
        .RuleFor(x => x.Digito, 5)
        .Generate();
    
    internal static DadosPessoaPersistenceModel PessoaId2 => _dadosPessoaFaker
        .RuleFor(x => x.Id, Guid.Parse("00000000-0000-0000-0000-000000000002"))
        .RuleFor(x => x.NomeCompleto, "JOHN DOE")
        .Generate();
}