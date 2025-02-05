using System;
using System.Collections.Generic;

namespace ApiGestionFacturas;

public partial class Invoice
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public DateTime? CreationDate { get; set; }

    public decimal Total { get; set; }

    public virtual Client Client { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
}
