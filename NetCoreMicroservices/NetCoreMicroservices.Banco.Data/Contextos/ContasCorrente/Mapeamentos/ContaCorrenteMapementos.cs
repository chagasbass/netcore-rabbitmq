using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCoreMicroservices.Banco.Dominio.Contextos.ContasCorrente.Entidades;

namespace NetCoreMicroservices.Banco.Data.Contextos.ContasCorrente.Mapeamentos
{
    public class ContaCorrenteMapementos : IEntityTypeConfiguration<ContaCorrente>
    {
        public void Configure(EntityTypeBuilder<ContaCorrente> builder)
        {
            builder.ToTable("TB_CONTA_CORRENTE");
            builder.HasKey(x => x.Id).IsClustered(true);
            builder.Property(x => x.TipoDeConta).HasColumnName("TIPO_CONTA")
                .IsRequired();

            builder.Property(x => x.Saldo).HasColumnName("SALDO")
                .HasColumnType("decimal");

        }
    }
}
