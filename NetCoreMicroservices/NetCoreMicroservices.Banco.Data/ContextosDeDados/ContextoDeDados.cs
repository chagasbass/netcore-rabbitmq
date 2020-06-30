using Microsoft.EntityFrameworkCore;
using NetCoreMicroservices.Banco.Data.Contextos.ContasCorrente.Mapeamentos;
using NetCoreMicroservices.Banco.Dominio.Contextos.ContasCorrente.Entidades;

namespace NetCoreMicroservices.Banco.Data.ContextosDeDados
{
    public class ContextoDeDados:DbContext
    {
        public DbSet<ContaCorrente> ContaCorrentes { get; set; }

        public ContextoDeDados(DbContextOptions options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContaCorrenteMapementos());
        }
    }
}
