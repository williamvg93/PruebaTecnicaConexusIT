using System;
using System.Collections.Generic;

namespace ApiGestionFacturas;

public partial class Client
{
    public int Id { get; set; }

    public string Cname { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
