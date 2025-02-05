using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGestionFacturas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Products__3214EC2774FE5B21");

            builder.Property(e => e.Id).HasColumnName("ID");
            builder.Property(e => e.Pname)
                .IsRequired()
                .HasMaxLength(80)
                .HasColumnName("PName");
            builder.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        }
    }
}