using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGestionFacturas.Dtos.Get;
using ApiGestionFacturas.Dtos.Post;
using AutoMapper;

namespace ApiGestionFacturas.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // GET
            CreateMap<Client, ClientGDTO>()
            .ForMember(dto => dto.Nombre, opt => opt.MapFrom(ent => ent.Cname))
            .ForMember(dto => dto.Correo, opt => opt.MapFrom(ent => ent.Email))
            .ForMember(dto => dto.NumeroTelefono, opt => opt.MapFrom(ent => ent.Phone));
            
            CreateMap<Product, ProductGDTO>()
            .ForMember(dto => dto.Nombre, opt => opt.MapFrom(ent => ent.Pname))
            .ForMember(dto => dto.Precio, opt => opt.MapFrom(ent => ent.Price))
            .ForMember(dto => dto.StockDisponible, opt => opt.MapFrom(ent => ent.Stock));
            
            CreateMap<Invoice, InvoiceGDTO>()
            .ForMember(dto => dto.IdFactura, opt => opt.MapFrom(ent => ent.Id))
            .ForMember(dto => dto.IdCliente, opt => opt.MapFrom(ent => ent.ClientId))
            .ForMember(dto => dto.FechaDeCreacion, opt => opt.MapFrom(ent => ent.CreationDate))
            .ForMember(dto => dto.ValorTotal, opt => opt.MapFrom(ent => ent.Total));

            CreateMap<InvoiceDetail, InvoiceDetailGDTO>()
            .ForMember(dto => dto.IdDetalleFactura, opt => opt.MapFrom(ent => ent.Id))
            .ForMember(dto => dto.IdFactura, opt => opt.MapFrom(ent => ent.InvoiceId))
            .ForMember(dto => dto.IdProducto, opt => opt.MapFrom(ent => ent.ProductId))
            .ForMember(dto => dto.Cantidad, opt => opt.MapFrom(ent => ent.Quantity))
            .ForMember(dto => dto.PrecioUnitario, opt => opt.MapFrom(ent => ent.UnitPrice));


            // POST
            CreateMap<Client, ClientPDTO>()
            .ForMember(dto => dto.Nombre, opt => opt.MapFrom(ent => ent.Cname))
            .ForMember(dto => dto.Correo, opt => opt.MapFrom(ent => ent.Email))
            .ForMember(dto => dto.NumeroTelefono, opt => opt.MapFrom(ent => ent.Phone));

            CreateMap<Product, ProductPDTO>()
            .ForMember(dto => dto.Nombre, opt => opt.MapFrom(ent => ent.Pname))
            .ForMember(dto => dto.Precio, opt => opt.MapFrom(ent => ent.Price))
            .ForMember(dto => dto.StockDisponible, opt => opt.MapFrom(ent => ent.Stock));

            CreateMap<Invoice, InvoicePDTO>()
            .ForMember(dto => dto.IdCliente, opt => opt.MapFrom(ent => ent.ClientId))
            .ForMember(dto => dto.ValorTotal, opt => opt.MapFrom(ent => ent.Total));

            CreateMap<InvoiceDetail, InvoiceDetailPDTO>()
            .ForMember(dto => dto.IdFactura, opt => opt.MapFrom(ent => ent.InvoiceId))
            .ForMember(dto => dto.IdProducto, opt => opt.MapFrom(ent => ent.ProductId))
            .ForMember(dto => dto.Cantidad, opt => opt.MapFrom(ent => ent.Quantity));
        }
    }
}