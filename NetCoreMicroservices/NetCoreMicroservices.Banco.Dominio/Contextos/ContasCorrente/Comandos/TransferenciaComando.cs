using MediatR;
using NetCoreRabbitMQ.Dominio.Core.Comandos;

namespace NetCoreMicroservices.Banco.Dominio.Contextos.ContasCorrente.Comandos
{
    public abstract class TransferenciaComando:Comando
    {
        public int ContaDe { get; protected set; }
        public int ContaPara { get; protected set; }
        public decimal ValorTransferencia { get; protected set; }
    }
}
