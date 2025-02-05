using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGestionFacturas;
using Domain.Interfaces;

namespace Application.Repository
{
    public class InvoiceRepository(ApiGestionFacturasContext context) : GenericRepository<Invoice>(context), IInvoice
    {
        private readonly ApiGestionFacturasContext _context = context;
    }
}