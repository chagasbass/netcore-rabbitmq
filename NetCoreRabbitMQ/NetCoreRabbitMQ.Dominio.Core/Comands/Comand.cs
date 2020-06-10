using NetCoreRabbitMQ.Dominio.Core.Events;
using System;

namespace NetCoreRabbitMQ.Dominio.Core.Comands
{
    public abstract class Comand:Message
    {
        public DateTime   Timestamp { get; protected set; }

        protected Comand()
            => Timestamp = DateTime.Now;
    }
}
