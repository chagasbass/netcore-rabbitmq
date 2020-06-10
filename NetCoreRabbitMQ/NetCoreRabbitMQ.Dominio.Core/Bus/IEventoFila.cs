using NetCoreRabbitMQ.Dominio.Core.Comands;
using NetCoreRabbitMQ.Dominio.Core.Events;
using System.Threading.Tasks;

namespace NetCoreRabbitMQ.Dominio.Core.Bus
{
    public interface IEventBus
    {
        Task SendCommand<T>(T comand) where T : Comand;
        void Publish<T>(T @event) where T :Event;
        void Subscribe<T,TH>() where T : Event where TH : IEventHandler<T>;
    }
  
}
