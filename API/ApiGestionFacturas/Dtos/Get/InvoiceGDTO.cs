using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGestionFacturas.Dtos.Get
{
    public class InvoiceGDTO
    {
        public int IdFactura { get; set; }
        public int IdCliente { get; set; }
        public DateTime? FechaDeCreacion { get; set; }
        public decimal ValorTotal { get; set; }

    }
}