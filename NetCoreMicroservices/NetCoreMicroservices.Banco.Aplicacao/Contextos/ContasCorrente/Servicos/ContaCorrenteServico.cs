using NetCoreMicroservices.Banco.Dominio.Contextos.ContasCorrente.Comandos;
using NetCoreMicroservices.Banco.Dominio.Contextos.ContasCorrente.Entidades;
using NetCoreMicroservices.Banco.Dominio.Contextos.ContasCorrente.Repositorios;
using NetCoreMicroservices.Banco.Dominio.Contextos.ContasCorrente.Servicos;
using NetCoreRabbitMQ.Dominio.Core.Filas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreMicroservices.Banco.Aplicacao.Contextos.ContasCorrente.Servicos
{
    public class ContaCorrenteServico : IContaCorrenteServico
    {
        private readonly IContaCorrenteRepositorio contaCorrenteRepositorio;
        private readonly IFilaDeEventos filaDeEventos;

        public ContaCorrenteServico(IContaCorrenteRepositorio contaCorrenteRepositorio, IFilaDeEventos filaDeEventos)
        {
            this.contaCorrenteRepositorio = contaCorrenteRepositorio;
            this.filaDeEventos = filaDeEventos;
        }

        public async Task<IEnumerable<ContaCorrente>> ListarContasCorrenteAsync()
        {
            return await contaCorrenteRepositorio.ListarContasCorrenteAsync();
        }

        public async Task TransferirFundosAsync(Transferencia transferencia)
        {
            var comandoTransferencia = new CriarTransferenciaComando(
                transferencia.ContaDe,
                transferencia.ContaPara,
                transferencia.ValorTranferencia);

            await filaDeEventos.EnviarComando(comandoTransferencia);

        }
    }
}
