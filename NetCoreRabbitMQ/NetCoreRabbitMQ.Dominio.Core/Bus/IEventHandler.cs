using NetCoreRabbitMQ.Domain.Core.Events;
using System.Threading.Tasks;

namespace NetCoreRabbitMQ.Domain.Core.Bus
{
    /// <summary>
    /// contrato para handler de eventos genericos
    /// Recebe qualquer tipo de evento
    /// </summary>
    /// <typeparam name=""></typeparam>
    public interface IEventHandler<in TEvent> : IEventHandler where TEvent : Event
    {
        Task Handle(TEvent @event);
    }

    /// <summary>
    /// contrato vazio
    /// </summary>
    public interface IEventHandler
    {

    }
}
