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
            .ForMember(dto => dto.NumeroTelefono, opt => opt.MapFrom(ent => ent.Phone))
            .ReverseMap()
            .ForMember(dto => dto.Cname, opt => opt.MapFrom(ent => ent.Nombre))
            .ForMember(dto => dto.Email, opt => opt.MapFrom(ent => ent.Correo))
            .ForMember(dto => dto.Phone, opt => opt.MapFrom(ent => ent.NumeroTelefono));

            CreateMap<Product, ProductPDTO>()
            .ForMember(dto => dto.Nombre, opt => opt.MapFrom(ent => ent.Pname))
            .ForMember(dto => dto.Precio, opt => opt.MapFrom(ent => ent.Price))
            .ForMember(dto => dto.StockDisponible, opt => opt.MapFrom(ent => ent.Stock))
            .ReverseMap()
            .ForMember(dto => dto.Pname, opt => opt.MapFrom(ent => ent.Nombre))
            .ForMember(dto => dto.Price, opt => opt.MapFrom(ent => ent.Precio))
            .ForMember(dto => dto.Stock, opt => opt.MapFrom(ent => ent.StockDisponible));

            CreateMap<Invoice, InvoicePDTO>()
            .ForMember(dto => dto.IdCliente, opt => opt.MapFrom(ent => ent.ClientId))
            .ForMember(dto => dto.ValorTotal, opt => opt.MapFrom(ent => ent.Total))
            .ReverseMap()
            .ForMember(dto => dto.ClientId, opt => opt.MapFrom(ent => ent.IdCliente))
            .ForMember(dto => dto.Total, opt => opt.MapFrom(ent => ent.ValorTotal));

            CreateMap<InvoiceDetail, InvoiceDetailPDTO>()
            .ForMember(dto => dto.IdFactura, opt => opt.MapFrom(ent => ent.InvoiceId))
            .ForMember(dto => dto.IdProducto, opt => opt.MapFrom(ent => ent.ProductId))
            .ForMember(dto => dto.Cantidad, opt => opt.MapFrom(ent => ent.Quantity))
            .ReverseMap()
            .ForMember(dto => dto.InvoiceId, opt => opt.MapFrom(ent => ent.IdFactura))
            .ForMember(dto => dto.ProductId, opt => opt.MapFrom(ent => ent.IdProducto))
            .ForMember(dto => dto.Quantity, opt => opt.MapFrom(ent => ent.Cantidad));
        }
    }
}