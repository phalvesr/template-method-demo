using System.Net;
using System.Text.Json;
using TemplateMethodDemo.TemplateMethod.Gateways;

namespace TemplateMethodDemo.TemplateMethod.Data.Web;

public class CadastroGateway : ICadastroGateway
{
    private readonly HttpClient _cadastroHttpClient;

    public CadastroGateway(IHttpClientFactory httpClientFactory)
    {
        _cadastroHttpClient = httpClientFactory.CreateClient("cadastro");
    }

    public async Task<DadosPessoa?> ObterDadosPessoaAsync(Guid idPessoa)
    {
        var response = await _cadastroHttpClient.GetAsync($"/pessoas/{idPessoa}");

        response.EnsureSuccessStatusCode();
        
        if (response.StatusCode is HttpStatusCode.NoContent)
        {
            return null!;
        }
        
        var json = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<DadosPessoa>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }
    
    public async Task<DadosBancarios?> ObterDadosBancariosAsync(Guid idPessoa)
    {
        var response = await _cadastroHttpClient.GetAsync($"/pessoas/{idPessoa}/bancos");

        response.EnsureSuccessStatusCode();
        
        if (response.StatusCode is HttpStatusCode.NoContent)
        {
            return null!;
        }
        
        var json = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<DadosBancarios>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }
}