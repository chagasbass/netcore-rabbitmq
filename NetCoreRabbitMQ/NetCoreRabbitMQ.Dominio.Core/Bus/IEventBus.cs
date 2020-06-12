using NetCoreRabbitMQ.Domain.Core.Comands;
using NetCoreRabbitMQ.Domain.Core.Events;
using System.Threading.Tasks;

namespace NetCoreRabbitMQ.Domain.Core.Bus
{
    /// <summary>
    ///  Contrato para Fila de eventos
    ///  Contém os contratos de envio de comando,publicação de eventos e subscrição a eventos
    ///  Mediator será usado para os envios de comandos
    ///  Feito de modo genérico para ser usado por qualquer tipo de evento
    /// </summary>
    public interface IEventBus
    {
        Task SendCommand<T>(T comand) where T : Comand;
        void Publish<T>(T @event) where T :Event;
        void Subscribe<T,TH>() where T : Event where TH : IEventHandler<T>;
    }
  
}
