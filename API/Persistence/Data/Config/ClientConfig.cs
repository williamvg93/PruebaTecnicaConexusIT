using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGestionFacturas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Config
{
    public class ClientConfig : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client");
            
            builder.HasKey(e => e.Id).HasName("PK__Client__3214EC2778B2EADC");

            builder.HasIndex(e => e.Email, "UQ__Client__A9D10534B1C72B6A").IsUnique();

            builder.Property(e => e.Id).HasColumnName("ID");
            builder.Property(e => e.Cname)
                .IsRequired()
                .HasMaxLength(80)
                .HasColumnName("CName");
            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(80);
            builder.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(25);
        }
    }
}