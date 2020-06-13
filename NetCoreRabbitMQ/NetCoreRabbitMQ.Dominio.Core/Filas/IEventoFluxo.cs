using NetCoreRabbitMQ.Dominio.Core.Eventos;
using System.Threading.Tasks;

namespace NetCoreRabbitMQ.Domain.Core.Bus
{
    /// <summary>
    /// contrato para handler de eventos genericos
    /// Recebe qualquer tipo de evento
    /// </summary>
    /// <typeparam name=""></typeparam>
    public interface IEventoFluxo<in TEvent> : IEventoFluxo where TEvent : Evento
    {
        Task ExecutarFluxo(TEvent evento);
    }

    /// <summary>
    /// contrato vazio
    /// </summary>
    public interface IEventoFluxo
    {

    }
}
