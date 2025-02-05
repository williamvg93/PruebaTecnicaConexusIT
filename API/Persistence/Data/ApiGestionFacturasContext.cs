using System;
using System.Collections.Generic;
using System.Reflection;
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

/*     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=GestionVentasDB;Integrated Security=True;TrustServerCertificate=True;"); */

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }

    /* partial void OnModelCreatingPartial(ModelBuilder modelBuilder); */
}
