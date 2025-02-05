using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGestionFacturas;
using Domain.Interfaces;

namespace Application.Repository
{
    public class InvoiceDetailRepository(ApiGestionFacturasContext context) : GenericRepository<InvoiceDetail>(context), IInvoiceDetail
    {
        private readonly ApiGestionFacturasContext _context = context;
    }
}