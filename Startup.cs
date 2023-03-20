using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
// using NSubstitute;
using RichardSzalay.MockHttp;
using TemplateMethodDemo.TemplateMethod;
using TemplateMethodDemo.TemplateMethod.Data.Database;
using TemplateMethodDemo.TemplateMethod.Data.Web;
using TemplateMethodDemo.TemplateMethod.Gateways;
using TemplateMethodDemo.TemplateMethod.Pattern;
using TemplateMethodDemo.Utils;

namespace TemplateMethodDemo;

public class Startup
{
    private static IServiceProvider? _serviceProvider = null;
    
    public static IServiceProvider ResolverServicos()
    {
        if (_serviceProvider is null)
        {
            var services = new ServiceCollection();
            
            services.AddSingleton(MockHttpClient);
            services.AddSingleton<ImportaCadastroPessoaGateway>();
            services.AddSingleton<AtualizacaoCadastral>();
            services.AddSingleton<InclusaoCadastral>();
            services.AddSingleton<ICadastroGateway, CadastroGateway>();

            services.AddSingleton<ImportaCadastroPessoaGateway>();
            services.AddSingleton<IImportaCadastroPessoaGateway>(sp => sp.GetRequiredService<ImportaCadastroPessoaGateway>());
            services.AddSingleton<ICadastroExistenteGateway>(sp => sp.GetRequiredService<ImportaCadastroPessoaGateway>());

            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            
            _serviceProvider = services.BuildServiceProvider();
        }

        return _serviceProvider;
    }

    private static IHttpClientFactory MockHttpClient(IServiceProvider services)
    {
        var httpClientFactory = Substitute.For<IHttpClientFactory>();
            
        var mockHttp = new MockHttpMessageHandler();

        mockHttp
            .When(HttpMethod.Get, "/pessoas/00000000-0000-0000-0000-000000000001")
            .Respond(HttpStatusCode.OK, JsonContent.Create(Mock.PessoaId1Web));

        mockHttp
            .When(HttpMethod.Get, "/pessoas/00000000-0000-0000-0000-000000000002")
            .Respond(HttpStatusCode.OK, JsonContent.Create(Mock.PessoaId2));

        mockHttp.When(HttpMethod.Get, "/pessoas/00000000-0000-0000-0000-000000000001/bancos")
            .Respond(HttpStatusCode.OK, JsonContent.Create(
                new DadosBancarios
                {
                    Id = Guid.Parse("52ee96ff-00d5-4a08-b578-161c6289cc5a"),
                    IdPessoa = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Agencia = "6611",
                    Conta = "45585",
                    Digito = 5
                }));
        
        mockHttp.When(HttpMethod.Get, "/pessoas/00000000-0000-0000-0000-000000000002/bancos")
            .Respond(HttpStatusCode.OK, JsonContent.Create(
                new DadosBancarios
                {
                    Id = Guid.Parse("81bcc841-00bf-4eea-9e1f-3eacfacf7878"),
                    IdPessoa = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Agencia = "6612",
                    Conta = "45582",
                    Digito = 2
                }));
        
        var httpClient = mockHttp.ToHttpClient();
        httpClient.BaseAddress = new Uri("http://mock-server.net");
        
        httpClientFactory.CreateClient("cadastro")
            .Returns(httpClient);
            
        return httpClientFactory;
    }
}