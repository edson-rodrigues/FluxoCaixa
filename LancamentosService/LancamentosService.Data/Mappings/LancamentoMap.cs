using LancamentosService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LancamentosService.Data.Mappings
{
    public class LancamentoMap : IEntityTypeConfiguration<Lancamento>
    {
        public void Configure(EntityTypeBuilder<Lancamento> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Lancamen__3214EC07F9F809EA");

            builder.ToTable("Lancamento");

            builder.Property(e => e.DataCriacao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            builder.Property(e => e.DataLancamento).HasColumnType("datetime");
            builder.Property(e => e.Descricao)
                .HasMaxLength(255)
                .IsUnicode(false);
            builder.Property(e => e.Tipo)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);
            builder.Property(e => e.Valor).HasColumnType("decimal(18, 2)");
        }
    }
}
