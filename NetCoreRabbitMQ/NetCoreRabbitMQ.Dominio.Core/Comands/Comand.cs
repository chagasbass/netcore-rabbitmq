using NetCoreRabbitMQ.Domain.Core.Events;
using System;

namespace NetCoreRabbitMQ.Domain.Core.Comands
{
    /// <summary>
    /// Entidade base para os comandos
    /// </summary>
    public abstract class Comand : Message
    {
        public DateTime Timestamp { get; protected set; }

        protected Comand()
            => Timestamp = DateTime.Now;
    }
}
