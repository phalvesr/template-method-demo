using Microsoft.Extensions.DependencyInjection;
using TemplateMethodDemo;
using TemplateMethodDemo.TemplateMethod.Data.Database;
using TemplateMethodDemo.TemplateMethod.Pattern;
using TemplateMethodDemo.Utils;

#region Resolucao servicos
var services = Startup.ResolverServicos();

var cadastros = services.GetRequiredService<ImportaCadastroPessoaGateway>();
var atualizacaoCadastral = services.GetRequiredService<AtualizacaoCadastral>();
var inclusaoCadastral = services.GetRequiredService<InclusaoCadastral>();

var ids = new Queue<Guid>(new []
{
    Guid.Parse("00000000-0000-0000-0000-000000000001"),
    Guid.Parse("00000000-0000-0000-0000-000000000002"),
});
#endregion

#region Print tabelas inicio
var cadastroPessoasAntes = cadastros.GetPessoas();
// var cadastroBancosAntes = cadastros.GetBancos();

ConsoleTable.Print(cadastroPessoasAntes, "Documentos");
// ConsoleTable.Print(cadastroBancosAntes, "Documentos");
#endregion

while (ids.Count > 0)
{
    var id = ids.Dequeue();
    
    var pessoaExistente = cadastros.ObterDadosPessoa(id);

    if (pessoaExistente is null)
    {
        await inclusaoCadastral.Cadastrar(id);
    }
    else
    {
        await atualizacaoCadastral.Cadastrar(id);
    }
}

#region Print tabelas fim
var cadastroPessoas = cadastros.GetPessoas();
// var cadastroBancos = cadastros.GetBancos();

ConsoleTable.Print(cadastroPessoas, "Documentos");
// ConsoleTable.Print(cadastroBancos, "Documentos");
#endregion

