using Microsoft.EntityFrameworkCore;
using NetCoreMicroservices.Banco.Data.ContextosDeDados;
using NetCoreMicroservices.Banco.Dominio.Contextos.ContasCorrente.Entidades;
using NetCoreMicroservices.Banco.Dominio.Contextos.ContasCorrente.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreMicroservices.Banco.Data.Contextos.ContasCorrente.Repositorios
{
    public class ContaCorrenteRepositorio : IContaCorrenteRepositorio
    {
        private readonly ContextoDeDados contexto;

        public ContaCorrenteRepositorio(ContextoDeDados contexto)
        {
            this.contexto = contexto;
        }

        public async Task<IEnumerable<ContaCorrente>> ListarContasCorrenteAsync()
            => await contexto.ContaCorrentes.AsNoTracking().ToListAsync();
    }
}
