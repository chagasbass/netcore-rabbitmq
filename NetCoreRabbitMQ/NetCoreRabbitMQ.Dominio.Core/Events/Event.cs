using System;

namespace NetCoreRabbitMQ.Domain.Core.Events
{
    /// <summary>
    /// Entidade base para eventos
    /// </summary>
    public abstract class Event
    {
        public DateTime  Timestamp { get; protected set; }

        protected Event()
            => Timestamp = DateTime.Now;
    }
}
