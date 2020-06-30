using NetCoreMicroservices.Banco.Dominio.Contextos.ContasCorrente.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreMicroservices.Banco.Dominio.Contextos.ContasCorrente.Repositorios
{
    public interface IContaCorrenteRepositorio
    {
        Task<IEnumerable<ContaCorrente>> ListarContasCorrenteAsync();
    }
}
