using ConsolidadoService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolidadoService.Data.Maps
{
    public class ConsolidadoMap : IEntityTypeConfiguration<Consolidado>
    {
        public void Configure(EntityTypeBuilder<Consolidado> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Consolid__3214EC0779FE6EC2");

            builder.ToTable("Consolidado");

            builder.Property(e => e.DataCriacao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            builder.Property(e => e.Saldo).HasColumnType("decimal(18, 2)");
        }
    }
}
