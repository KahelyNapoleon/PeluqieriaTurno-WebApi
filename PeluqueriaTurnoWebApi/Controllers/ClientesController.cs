using BLL.Services.Interfaces;
using DAL.Repositorios.Interfaces;
using DomainLayer.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PeluqueriaTurnoWebApi.DTOs.ClienteDTOs;
using PeluqueriaTurnoWebApi.Mappings;

namespace PeluqueriaTurnoWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteReadDTO>>> GetClientes()
        {
            var clientes = await _clienteService.GetAll();
            if (!clientes.IsValid)
            {
                return NotFound(clientes.Errors);
            }

            //Convierto las lista de Cliente a ClienteDTO.
            var clienteDtoToList = clientes.Data!.Select(c => c!.ToReadDTO()); 

            return Ok(clienteDtoToList);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClienteReadDTO>> GetCliente([FromRoute] int id)
        {
            var cliente = await _clienteService.GetById(id);
            if(!cliente.IsValid)
            {
                return BadRequest(cliente.Errors);
            }

            var clienteDto = cliente.Data!.ToReadDTO();

            return Ok(clienteDto);
        }

        [HttpPost]
        public async Task<ActionResult<ClienteCreateDTO>> AddCliente([FromBody] ClienteCreateDTO cliente)
        {
            var clienteToEntity = cliente.ToEntity();
            var agregarCliente = await _clienteService.Add(clienteToEntity);
            if (!agregarCliente.IsValid)
            {
                return BadRequest(agregarCliente.Errors);
            }
            return CreatedAtAction(nameof(GetCliente), new { id = agregarCliente.Data!.ClienteId }, agregarCliente.Data);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateCliente([FromBody] ClienteUpdateDTO cliente, [FromRoute]int id)
        {
            var clienteEntity = new Cliente();
            cliente.UpdateCliente(clienteEntity);

            var actualizarCliente = await _clienteService.Update(id,clienteEntity);
            if (!actualizarCliente.IsValid)
            {
                return BadRequest(actualizarCliente.Errors);
            }
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCliente([FromRoute] int id)
        {
            var eliminarCliente = await _clienteService.Delete(id);
            if (!eliminarCliente.IsValid)
            {
                return NotFound(eliminarCliente.Errors);
            }

            return NoContent();
        }

    }
}
