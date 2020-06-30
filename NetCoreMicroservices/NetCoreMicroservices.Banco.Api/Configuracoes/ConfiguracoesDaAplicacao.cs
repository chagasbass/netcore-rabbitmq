using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreMicroservices.Banco.Api.Middlewares;
using System.Linq;

namespace NetCoreMicroservices.Banco.Api.Configuracoes
{
    public static class ConfiguracoesDaAplicacao
    {
        public static void ConfigurarGerenciamentoDeAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            //var configuracoesAppSettings = new FirebaseAutenticacao();

            //new ConfigureFromConfigurationOptions<FirebaseAutenticacao>(
            // configuration.GetSection("FirebaseAutenticacao"))
            //     .Configure(configuracoesAppSettings);

            //services.AddSingleton(configuracoesAppSettings);
        }

        /// <summary>
        /// Insere a compressão Brotli nas requisiçoes
        /// </summary>
        /// <param name="services"></param>
        public static void InserirCompressaoDeRequisicoes(this IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.EnableForHttps = true;
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
            });
        }

        /// <summary>
        /// Configuração do novo serializador JSON do net Core
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigurarSerializacaoDeJson(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(opcoes =>
            {
                var serializerOptions = opcoes.JsonSerializerOptions;
                serializerOptions.IgnoreNullValues = true;
                serializerOptions.IgnoreReadOnlyProperties = true;
                serializerOptions.WriteIndented = true;
            });
        }

        /// <summary>
        /// Configura o GDPR para a API
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigurarGDPR(this IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }

        public static void AddGlobalExceptionHandlerMiddleware(this IServiceCollection services)
            => services.AddTransient<GlobalExceptionHandlerMiddleware>();

        public static void UseGlobalExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }
    }
}
