using NetCoreRabbitMQ.Domain.Core.Comands;
using NetCoreRabbitMQ.Domain.Core.Events;
using System.Threading.Tasks;

namespace NetCoreRabbitMQ.Domain.Core.Bus
{
    public interface IEventBus
    {
        Task SendCommand<T>(T comand) where T : Comand;
        void Publish<T>(T @event) where T :Event;
        void Subscribe<T,TH>() where T : Event where TH : IEventHandler<T>;
    }
  
}
