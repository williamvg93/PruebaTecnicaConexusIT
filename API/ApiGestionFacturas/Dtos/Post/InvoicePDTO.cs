using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGestionFacturas.Dtos.Post
{
    public class InvoicePDTO
    {
        public int IdCliente { get; set; }
        public decimal ValorTotal { get; set; }
    }
}