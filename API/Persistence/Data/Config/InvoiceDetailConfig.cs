using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGestionFacturas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Config
{
    public class InvoiceDetailConfig : IEntityTypeConfiguration<InvoiceDetail>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
        {
            builder.ToTable("InvoiceDetail", tb => tb.HasTrigger("trg_UpdateStock"));

            builder.HasKey(e => e.Id).HasName("PK__InvoiceD__3214EC27B7CBD959");

            builder.HasIndex(e => e.ProductId, "idx_InvoiceDetail_ProductID");

            builder.Property(e => e.Id).HasColumnName("ID");
            builder.Property(e => e.SubTotal)
                .HasComputedColumnSql("([Quantity]*[UnitPrice])", false)
                .HasColumnType("decimal(21, 2)");
            builder.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

            builder.HasOne(d => d.Invoice).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceDetail_Invoice");

            builder.HasOne(d => d.Product).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceDetail_Product");
        }
    }
}