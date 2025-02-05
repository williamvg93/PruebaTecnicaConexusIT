using System;
using System.Collections.Generic;

namespace ApiGestionFacturas;

public partial class Product
{
    public int Id { get; set; }

    public string Pname { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
}
