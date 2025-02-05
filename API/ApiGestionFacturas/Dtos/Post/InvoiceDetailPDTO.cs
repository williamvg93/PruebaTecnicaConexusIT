using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGestionFacturas.Dtos.Post
{
    public class InvoiceDetailPDTO
    {
        public int IdFactura { get; set; }w
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal? SubTotal { get; set; }
    }
}