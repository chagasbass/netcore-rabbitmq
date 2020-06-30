using NetCoreMicroservices.Banco.Dominio.Contextos.ContasCorrente.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreMicroservices.Banco.Dominio.Contextos.ContasCorrente.Servicos
{
    public interface IContaCorrenteServico
    {
        Task<IEnumerable<ContaCorrente>> ListarContasCorrenteAsync();
        Task TransferirFundosAsync(Transferencia transferencia);
    }
}
