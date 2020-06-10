using NetCoreRabbitMQ.Domain.Core.Events;
using System;

namespace NetCoreRabbitMQ.Domain.Core.Comands
{
    public abstract class Comand:Message
    {
        public DateTime   Timestamp { get; protected set; }

        protected Comand()
            => Timestamp = DateTime.Now;
    }
}
