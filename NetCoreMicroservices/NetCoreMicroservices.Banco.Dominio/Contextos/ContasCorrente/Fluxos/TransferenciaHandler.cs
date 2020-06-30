using MediatR;
using NetCoreMicroservices.Banco.Dominio.Contextos.ContasCorrente.Comandos;
using NetCoreMicroservices.Banco.Dominio.Contextos.ContasCorrente.Eventos;
using NetCoreRabbitMQ.Dominio.Core.Filas;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreMicroservices.Banco.Dominio.Contextos.ContasCorrente.Fluxos
{
    /// <summary>
    /// Handler publicam eventos no rabbitMQ
    /// </summary>
    public class TransferenciaHandler : IRequestHandler<CriarTransferenciaComando, bool>
    {
        private readonly IFilaDeEventos filaDeEventos;

        public TransferenciaHandler(IFilaDeEventos filaDeEventos)
        {
            this.filaDeEventos = filaDeEventos;
        }

        public async Task<bool> Handle(CriarTransferenciaComando request, CancellationToken cancellationToken)
        {
            var eventoDeTransferencia = new EventoDeCriacaoDeTransferencia(
                request.ContaDe,
                request.ContaPara,
                request.ValorTransferencia);

             filaDeEventos.PublicarEvento(eventoDeTransferencia);

            return await Task.FromResult(true);
        }
    }
}
