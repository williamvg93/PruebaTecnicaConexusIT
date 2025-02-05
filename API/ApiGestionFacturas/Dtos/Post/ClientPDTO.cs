using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGestionFacturas.Dtos.Post
{
    public class ClientPDTO
    {
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string NumeroTelefono { get; set; }
    }
}