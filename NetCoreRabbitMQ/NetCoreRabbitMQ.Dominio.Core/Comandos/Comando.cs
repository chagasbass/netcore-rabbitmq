
using NetCoreRabbitMQ.Dominio.Core.Eventos;
using System;

namespace NetCoreRabbitMQ.Dominio.Core.Comandos
{
    /// <summary>
    /// Entidade base para os comandos
    /// </summary>
    public abstract class Comando : Mensagem
    {
        public DateTime DataEnvio { get; protected set; }

        protected Comando()
            => DataEnvio = DateTime.Now;
    }
}
