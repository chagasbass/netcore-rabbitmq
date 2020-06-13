using NetCoreRabbitMQ.Domain.Core.Bus;
using NetCoreRabbitMQ.Dominio.Core.Comandos;
using NetCoreRabbitMQ.Dominio.Core.Eventos;
using System.Threading.Tasks;

namespace NetCoreRabbitMQ.Dominio.Core.Filas
{
    /// <summary>
    ///  Contrato para Fila de eventos
    ///  Contém os contratos de envio de comando,publicação de eventos e subscrição a eventos
    ///  Mediator será usado para os envios de comandos
    ///  Feito de modo genérico para ser usado por qualquer tipo de evento
    /// </summary>
    public interface IFilaDeEventos
    {
        Task EnviarComando<T>(T comando) where T : Comando;
        void PublicarEvento<T>(T evento) where T :Evento;
        void EfetuarInscricao<T,TH>() where T : Evento where TH : IEventoFluxo<T>;
    }
  
}
