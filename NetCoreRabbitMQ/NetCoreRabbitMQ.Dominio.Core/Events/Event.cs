using System;

namespace NetCoreRabbitMQ.Dominio.Core.Events
{
    public abstract class Event
    {
        public DateTime  Timestamp { get; protected set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
