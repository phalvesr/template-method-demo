using TemplateMethodDemo.TemplateMethod.Data.Database;
using TemplateMethodDemo.TemplateMethod.Gateways;
using TaskExtensions = TemplateMethodDemo.TemplateMethod.Extensions.TaskExtensions;

namespace TemplateMethodDemo.TemplateMethod.Pattern;

public abstract class CadastroClienteBase
{
    private readonly ICadastroGateway _cadastroGateway;

    public CadastroClienteBase(ICadastroGateway cadastroGateway)
    {
        _cadastroGateway = cadastroGateway;
    }
    
    public async Task<bool> Cadastrar(Guid idPessoa)
    {
        var dadosBancariosTask = ObterDadosBancariosAsync(idPessoa);
        var dadosPessoaTask = ObterDadosPessoaAsync(idPessoa);

        var tasksExecutionResult = await TaskExtensions.WhenAllHandlingException(dadosBancariosTask, dadosPessoaTask);
        
        if (!tasksExecutionResult.IsSuccess)
        {
            return false;
        }

        var dadosBancarios =  dadosBancariosTask.Result;
        var dadosPessoa = dadosPessoaTask.Result;

        if (!dadosBancarios.IsSuccess || !dadosPessoa.IsSuccess)
        {
            return false;
        }

        var pessoa = OnMapeamentoPessoaAsync(dadosPessoa.Data);
        var banco = OnMapeamentoBancoAsync(dadosBancarios.Data);

        await OnPersistirDadosAsync(pessoa, banco);
        
        return true;
    }

    protected abstract DadosPessoaPersistenceModel OnMapeamentoPessoaAsync(DadosPessoa dadosPessoa);
    protected abstract DadosBancariosPersistenceModel OnMapeamentoBancoAsync(DadosBancarios dadosBancarios);
    protected abstract Task OnPersistirDadosAsync(DadosPessoaPersistenceModel novosDadosPessoa, DadosBancariosPersistenceModel novosDadosBancario);


    private async Task<Result<DadosBancarios>> ObterDadosBancariosAsync(Guid idPessoa)
    {
        return Result<DadosBancarios>.OfNullable(await _cadastroGateway.ObterDadosBancariosAsync(idPessoa));
    }

    private async Task<Result<DadosPessoa>> ObterDadosPessoaAsync(Guid idPessoa)
    {
        return Result<DadosPessoa>.OfNullable(await _cadastroGateway.ObterDadosPessoaAsync(idPessoa));
    }
}