using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGestionFacturas;
using Application.Repository;
using Domain.Interfaces;

namespace Application.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApiGestionFacturasContext _context;
        private IClient _clients;
        private IProduct _products;
        private IInvoice _invoices;
        private IInvoiceDetail _invoicesDetail;
    
    
        public UnitOfWork(ApiGestionFacturasContext context)
        {
            _context = context;
        }
    
        public IClient Clients
        {
            get
            {
                _clients ??= new ClientRepository(_context);
                return _clients;
            }
        }

        public IProduct Products
        {
            get
            {
                _products ??= new ProductRepository(_context);
                return _products;
            }
        }

        public IInvoice Invoices
        {
            get
            {
                _invoices ??= new InvoiceRepository(_context);
                return _invoices;
            }
        }
    
        public IInvoiceDetail InvoicesDetail
        {
            get
            {
                _invoicesDetail ??= new InvoiceDetailRepository(_context);
                return _invoicesDetail;
            }
        }


        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}