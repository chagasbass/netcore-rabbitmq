using Microsoft.Extensions.DependencyInjection;
using NetCoreRabbitMQ.Domain.Core.Bus;
using NetCoreRabbitMQ.Infrastructure.Bus.Bus;

namespace NetCoreRabbitMQ.Infrastructure.IoC.Containers
{
    public static class DependencyContainer
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IEventBus, RabbitMQBus>();
        }
    }
}
