using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetCoreMicroservices.Banco.Api.Configuracoes;
using NetCoreMicroservices.Ioc.Servicos;

namespace NetCoreMicroservices.Banco.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigurarContextoDeDados(Configuration);
            services.ResolverDependenciasDeRepositorios();
            services.ResolverDependenciasDeFila();
            services.ResolverDependenciasDeServicos();
            services.AddCors();

            services.AddMediatR(typeof(Startup));

            services.AddGlobalExceptionHandlerMiddleware();
            services.InserirCompressaoDeRequisicoes();
            services.ConfigurarSerializacaoDeJson();
            services.ConfigurarGDPR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x =>
             x.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseResponseCompression();

            app.UseGlobalExceptionHandlerMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
