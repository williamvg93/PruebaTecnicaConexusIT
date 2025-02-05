using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGestionFacturas.Dtos.Post
{
    public class ProductPDTO
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int StockDisponible { get; set; }
    }
}