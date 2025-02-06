using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGestionFacturas.Dtos.Get;
using ApiGestionFacturas.Dtos.Post;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiGestionFacturas.Controllers
{
    public class InvoiceController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
    
        public InvoiceController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    
        /* Get all Data from Table */
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<InvoiceGDTO>>> Get()
        {
            try
            {
                var invoices = await _unitOfWork.Invoices.GetAllAsync();
                return _mapper.Map<List<InvoiceGDTO>>(invoices);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrio un Error: {ex.Message}");
                return StatusCode(500, new { message = "Error inserperado al procesar la solicitud" });
            }
        }
    
        /* Get Data by ID */
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InvoiceGDTO>> Get(int id)
        {
            try
            {
                var invoice = await _unitOfWork.Invoices.GetByIdAsync(id);
                if (invoice == null) return NotFound(new { message = $"No se encontraron Facturas con el id: {id}" });
                return _mapper.Map<InvoiceGDTO>(invoice);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrio un Error: {ex.Message}");
                return StatusCode(500, new { message = "Error inserperado al procesar la solicitud" });
            }
        }
    
        /* Add a new Data in the Table */
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Invoice>> Post(InvoicePDTO invoicePdto)
        {
            try
            {
                var invoice = _mapper.Map<Invoice>(invoicePdto);
                _unitOfWork.Invoices.Add(invoice);
                await _unitOfWork.SaveAsync();
                if (invoice == null) return BadRequest();
                return CreatedAtAction(nameof(Post), new { id = invoice.Id }, invoicePdto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrio un Error: {ex.Message}");
                return StatusCode(500, new { message = "Error inserperado al procesar la solicitud" });
            }
        }
    
        /* Update Data By ID  */
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InvoicePDTO>> Put(int id, [FromBody] InvoicePDTO invoicePdto)
        {
            try
            {
                var invoice = _mapper.Map<Invoice>(invoicePdto);
                if (invoice.Id == 0) invoice.Id = id;
                if (invoice.Id != id) return BadRequest();
                if (invoice == null) return NotFound(new { message = $"No se encontraron Facturas con el id: {id}" });

                _unitOfWork.Invoices.Update(invoice);
                await _unitOfWork.SaveAsync();
                return invoicePdto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrio un Error: {ex.Message}");
                return StatusCode(500, new { message = "Error inserperado al procesar la solicitud" });
            }
        }
    
        /* Delete Data By ID */
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var invoice = await _unitOfWork.Invoices.GetByIdAsync(id);
                if (invoice == null) return NotFound(new { message = $"No se encontraron Facturas con el id: {id}" });
                _unitOfWork.Invoices.Remove(invoice);
                await _unitOfWork.SaveAsync();
                return Ok($"La factura con ID: ({invoice.Id}) fué eliminada Correctamente !!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrio un Error: {ex.Message}");
                return StatusCode(500, new { message = "Error inserperado al procesar la solicitud" });
            }
        }
    }
}