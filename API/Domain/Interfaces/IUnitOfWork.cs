using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IClient Clients {get;}
        IProduct Products {get;}
        IInvoice Invoices {get;}
        IInvoiceDetail InvoicesDetail {get;}
        Task<int> SaveAsync();
    }
}