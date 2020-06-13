using System;

namespace NetCoreRabbitMQ.Dominio.Core.Eventos
{
    /// <summary>
    /// Entidade base para eventos
    /// </summary>
    public abstract class Evento
    {
        public DateTime  DataEnvio { get; protected set; }

        protected Evento()
            => DataEnvio = DateTime.Now;
    }
}
