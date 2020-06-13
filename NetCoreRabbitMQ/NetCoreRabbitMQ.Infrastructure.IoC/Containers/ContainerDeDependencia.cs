using Microsoft.Extensions.DependencyInjection;
using NetCoreRabbitMQ.Dominio.Core.Filas;
using NetCoreRabbitMQ.Infraestrutura.Fila.Filas;

namespace NetCoreRabbitMQ.Infraestrutura.Ioc.Containers
{
    public static class ContainerDeDependencia
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IFilaDeEventos, RabbitMQFila>();
        }
    }
}
