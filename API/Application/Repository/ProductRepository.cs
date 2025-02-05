using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGestionFacturas;
using Domain.Interfaces;

namespace Application.Repository
{
    public class ProductRepository(ApiGestionFacturasContext context) : GenericRepository<Product>(context), IProduct
    {
        private readonly ApiGestionFacturasContext _context = context;
    }
}