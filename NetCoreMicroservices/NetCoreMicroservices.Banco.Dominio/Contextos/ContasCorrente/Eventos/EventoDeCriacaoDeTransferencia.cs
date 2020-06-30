using NetCoreRabbitMQ.Dominio.Core.Eventos;

namespace NetCoreMicroservices.Banco.Dominio.Contextos.ContasCorrente.Eventos
{
    public class EventoDeCriacaoDeTransferencia:Evento
    {
        public int ContaDe { get; private set; }
        public int ContaPara { get; private set; }
        public decimal ValorTrasferencia { get; private set; }

        public EventoDeCriacaoDeTransferencia(int contaDe, int contaPara, decimal valorTrasferencia)
        {
            ContaDe = contaDe;
            ContaPara = contaPara;
            ValorTrasferencia = valorTrasferencia;
        }
    }
}
