using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGestionFacturas;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository
{
    public class ClientRepository(ApiGestionFacturasContext context) : GenericRepository<Client>(context), IClient
    {
        private readonly ApiGestionFacturasContext _context = context;

        public async Task<Client> IfEmailExis(string email)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Email == email);
           
        }
    }
}