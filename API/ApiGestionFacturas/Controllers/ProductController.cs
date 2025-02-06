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
    public class ProductController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
    
        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    
        /* Get all Data from Table */
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProductGDTO>>> Get()
        {
            try
            {
                var products = await _unitOfWork.Products.GetAllAsync();
                return _mapper.Map<List<ProductGDTO>>(products);
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
        public async Task<ActionResult<ProductGDTO>> Get(int id)
        {
            try
            {
                var product = await _unitOfWork.Products.GetByIdAsync(id);
                if (product == null) return NotFound(new { message = $"No se encontraron Productos con el id: {id}" });
                return _mapper.Map<ProductGDTO>(product);
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
        public async Task<ActionResult<Product>> Post(ProductPDTO productPdto)
        {
            try
            {
                var product = _mapper.Map<Product>(productPdto);
                if (product == null) return BadRequest();
                _unitOfWork.Products.Add(product);
                await _unitOfWork.SaveAsync();
                return CreatedAtAction(nameof(Post), new { id = product.Id }, productPdto);
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
        public async Task<ActionResult<ProductPDTO>> Put(int id, [FromBody] ProductPDTO productPdto)
        {

            try
            {
                var product = _mapper.Map<Product>(productPdto);
                if (product.Id == 0) product.Id = id;
                if (product.Id != id) return BadRequest();
                if (product == null) return NotFound(new { message = $"No se encontraron Productos con el id: {id}" });
                _unitOfWork.Products.Update(product);
                await _unitOfWork.SaveAsync();
                return productPdto;
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
                var product = await _unitOfWork.Products.GetByIdAsync(id);
                if (product == null) return NotFound(new { message = $"No se encontraron Productos con el id: {id}" });
                _unitOfWork.Products.Remove(product);
                await _unitOfWork.SaveAsync();
                return Ok($"El Producto({product.Pname}) fué eliminado Correctamente !!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrio un Error: {ex.Message}");
                return StatusCode(500, new { message = "Error inserperado al procesar la solicitud" });
            }
        }
    }
}