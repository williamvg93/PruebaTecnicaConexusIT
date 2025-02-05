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
    public class ClientController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
    
        public ClientController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    
        /* Get all Data from Table */
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ClientGDTO>>> Get()
        {
            try
            {
                var clients = await _unitOfWork.Clients.GetAllAsync();
                return _mapper.Map<List<ClientGDTO>>(clients);
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
        public async Task<ActionResult<ClientGDTO>> Get(int id)
        {
            try
            {
                var client = await _unitOfWork.Clients.GetByIdAsync(id);
                if (client == null) return NotFound(new { message = $"No se encontraron clientes con el id: {id}" });
                return _mapper.Map<ClientGDTO>(client);
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
        public async Task<ActionResult<Client>> Post(ClientPDTO clientPDTO)
        {
            try
            {
                var checkEmail = await _unitOfWork.Clients.IfEmailExis(clientPDTO.Correo);

                if (checkEmail != null) return BadRequest(new { message = $"El correo electronico: ({clientPDTO.Correo}) ya se encuentra en uso, ingrese un Correo Diferente !!!"});

                var client = _mapper.Map<Client>(clientPDTO);
                _unitOfWork.Clients.Add(client);
                await _unitOfWork.SaveAsync();
                if (client == null) return BadRequest();

                return CreatedAtAction(nameof(Post), new { id = client.Id }, clientPDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrio un Error: {ex.Message}");
                
                return StatusCode(500, new { message = "Error inserperado al procesar la solicitud"});
            }
            
        }
    
        /* Update Data By ID  */
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClientPDTO>> Put(int id, [FromBody] ClientPDTO clientPDTO)
        {
            try
            {
                var client = _mapper.Map<Client>(clientPDTO);
                if (client.Id == 0) client.Id = id;
                if (client.Id != id) return BadRequest();
                if (client == null) return NotFound(new { message = $"No se encontraron clientes con el id: {id}" });

                _unitOfWork.Clients.Update(client);
                await _unitOfWork.SaveAsync();
                return clientPDTO;
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
                var client = await _unitOfWork.Clients.GetByIdAsync(id);
                if (client == null) return NotFound(new {message = $"No se encontraron clientes con el id: {id}"});
                _unitOfWork.Clients.Remove(client);
                await _unitOfWork.SaveAsync();
                return Ok($"El cliente({client.Cname}) fué eliminado Correctamente !!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrio un Error: {ex.Message}");
                return StatusCode(500, new { message = "Error inserperado al procesar la solicitud" });
            }
        }
    }
}