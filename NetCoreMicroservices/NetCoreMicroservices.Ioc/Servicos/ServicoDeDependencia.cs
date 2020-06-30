using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreMicroservices.Banco.Aplicacao.Contextos.ContasCorrente.Servicos;
using NetCoreMicroservices.Banco.Data.Contextos.ContasCorrente.Repositorios;
using NetCoreMicroservices.Banco.Data.ContextosDeDados;
using NetCoreMicroservices.Banco.Dominio.Contextos.ContasCorrente.Repositorios;
using NetCoreMicroservices.Banco.Dominio.Contextos.ContasCorrente.Servicos;
using NetCoreRabbitMQ.Dominio.Core.Filas;
using NetCoreRabbitMQ.Infraestrutura.Fila.Filas;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetCoreMicroservices.Banco.Dominio.Contextos.ContasCorrente.Fluxos;

namespace NetCoreMicroservices.Ioc.Servicos
{
    public static class ServicoDeDependencia
    {
        public static void ConfigurarContextoDeDados(this IServiceCollection services, IConfiguration configuration)
        {
            var configurationSection = configuration.GetConnectionString("bancoTeste");

            services.AddDbContext<ContextoDeDados>(contexto => contexto.UseSqlServer(configurationSection));
        }

        public static void ResolverDependenciasDeRepositorios(this IServiceCollection services)
        {
            services.AddTransient<IContaCorrenteRepositorio, ContaCorrenteRepositorio>();
        }

        public static void ResolverDependenciasDeServicos(this IServiceCollection services)
        {
            services.AddTransient<IContaCorrenteServico, ContaCorrenteServico>();
        }

        public static void ResolverDependenciasDeHandlers(this IServiceCollection services)
        {
            services.AddMediatR(typeof(TransferenciaHandler).Assembly);
        }

        public static void ResolverDependenciasDeFila(this IServiceCollection services)
        {
            services.AddTransient<IFilaDeEventos, RabbitMQFila>();
        }
    }
}
