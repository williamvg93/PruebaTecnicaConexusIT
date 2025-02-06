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
    public class InvoiceDetailController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
    
        public InvoiceDetailController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    
        /* Get all Data from Table */
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<InvoiceDetailGDTO>>> Get()
        {
            try
            {
                var invoicesDetail = await _unitOfWork.InvoicesDetail.GetAllAsync();
                return _mapper.Map<List<InvoiceDetailGDTO>>(invoicesDetail);
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
        public async Task<ActionResult<InvoiceDetailGDTO>> Get(int id)
        {
            try
            {
                var invoiceDetail = await _unitOfWork.InvoicesDetail.GetByIdAsync(id);
                if (invoiceDetail == null) return NotFound(new { message = $"No se encontraron DetallesDeFacturas con el id: {id}" });
                return _mapper.Map<InvoiceDetailGDTO>(invoiceDetail);
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
        public async Task<ActionResult<InvoiceDetail>> Post(InvoiceDetailPDTO invoiceDetailPDTO)
        {
            try
            {
                var invoiceDetail = _mapper.Map<InvoiceDetail>(invoiceDetailPDTO);
                _unitOfWork.InvoicesDetail.Add(invoiceDetail);
                await _unitOfWork.SaveAsync();
                if (invoiceDetail == null) return BadRequest();
                return CreatedAtAction(nameof(Post), new { id = invoiceDetail.Id }, invoiceDetailPDTO);
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
        public async Task<ActionResult<InvoiceDetailPDTO>> Put(int id, [FromBody] InvoiceDetailPDTO invoiceDetailPDTO)
        {
            try
            {
                var invoiceDetail = _mapper.Map<InvoiceDetail>(invoiceDetailPDTO);
                if (invoiceDetail.Id == 0) invoiceDetail.Id = id;
                if (invoiceDetail.Id != id) return BadRequest();
                if (invoiceDetail == null) return NotFound(new { message = $"No se encontraron DetallesDeFacturas con el id: {id}" });

                _unitOfWork.InvoicesDetail.Update(invoiceDetail);
                await _unitOfWork.SaveAsync();
                return invoiceDetailPDTO;
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
                var invoiceDetail = await _unitOfWork.InvoicesDetail.GetByIdAsync(id);
                if (invoiceDetail == null) return NotFound(new { message = $"No se encontraron DetallesDeFacturas con el id: {id}" });
                _unitOfWork.InvoicesDetail.Remove(invoiceDetail);
                await _unitOfWork.SaveAsync();
                return Ok($"El DetalleDeFactura con ID: ({invoiceDetail.Id}) fué eliminado Correctamente !!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrio un Error: {ex.Message}");
                return StatusCode(500, new { message = "Error inserperado al procesar la solicitud" });
            }
        }
    }
}