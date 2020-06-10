using NetCoreRabbitMQ.Domain.Core.Events;
using System.Threading.Tasks;

namespace NetCoreRabbitMQ.Domain.Core.Bus
{
    /// <summary>
    /// interface de eventos genericos
    /// </summary>
    /// <typeparam name=""></typeparam>
    public interface IEventHandler<in TEvent> : IEventHandler where TEvent : Event
    {
        Task Handle(TEvent @event);
    }

    public interface IEventHandler
    {

    }
}
