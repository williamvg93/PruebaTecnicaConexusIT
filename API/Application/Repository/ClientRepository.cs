using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGestionFacturas;
using Domain.Interfaces;

namespace Application.Repository
{
    public class ClientRepository(ApiGestionFacturasContext context) : GenericRepository<Client>(context), IClient
    {
        private readonly ApiGestionFacturasContext _context = context;
    }
}