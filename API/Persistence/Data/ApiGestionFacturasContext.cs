using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiGestionFacturas;

public partial class ApiGestionFacturasContext : DbContext
{
    public ApiGestionFacturasContext()
    {
    }

    public ApiGestionFacturasContext(DbContextOptions<ApiGestionFacturasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=GestionVentasDB;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Client__3214EC2778B2EADC");

            entity.ToTable("Client");

            entity.HasIndex(e => e.Email, "UQ__Client__A9D10534B1C72B6A").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cname)
                .IsRequired()
                .HasMaxLength(80)
                .HasColumnName("CName");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(80);
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(25);
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Invoice__3214EC27AD0B0CC7");

            entity.ToTable("Invoice");

            entity.HasIndex(e => e.ClientId, "idx_Invoice_ClientId");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Client).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceClient");
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__InvoiceD__3214EC27B7CBD959");

            entity.ToTable("InvoiceDetail", tb => tb.HasTrigger("trg_UpdateStock"));

            entity.HasIndex(e => e.ProductId, "idx_InvoiceDetail_ProductID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SubTotal)
                .HasComputedColumnSql("([Quantity]*[UnitPrice])", false)
                .HasColumnType("decimal(21, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceDetail_Invoice");

            entity.HasOne(d => d.Product).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceDetail_Product");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC2774FE5B21");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Pname)
                .IsRequired()
                .HasMaxLength(80)
                .HasColumnName("PName");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
