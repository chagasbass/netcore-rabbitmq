using MediatR;

namespace NetCoreRabbitMQ.Dominio.Core.Eventos
{
    /// <summary>
    /// Entidade base para as mensagens
    /// Esta classe implementa o IRequest pois será a entrada de dados(comando)
    /// Mensagem do Evento
    /// </summary>
    public abstract class Mensagem : IRequest<bool>
    {
        public string TipoMensagem { get; protected set; }

        protected Mensagem()
            => TipoMensagem = GetType().Name;
    }
}
