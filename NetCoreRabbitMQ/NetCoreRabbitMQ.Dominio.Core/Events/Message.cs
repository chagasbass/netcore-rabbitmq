using MediatR;

namespace NetCoreRabbitMQ.Domain.Core.Events
{
    /// <summary>
    /// Entidadd base para as mensagens
    /// Esta classe implementa o IRequest pois será a entrada de dados(comando)
    /// Mensagem do Evento
    /// </summary>
    public abstract class Message : IRequest<bool>
    {
        public string MessageType { get; protected set; }

        protected Message()
            => MessageType = GetType().Name;
    }
}
