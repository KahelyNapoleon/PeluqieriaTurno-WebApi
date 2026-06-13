using BLL.Services.Interfaces;
using Contracts.DTOs.ClienteDTOs;
using DAL.Repositorios.Interfaces;
using DomainLayer.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


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


            return Ok(clientes);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClienteReadDTO>> GetCliente([FromRoute] int id)
        {
            var cliente = await _clienteService.GetById(id);
            if(!cliente.IsValid)
            {
                return NotFound(cliente.Errors);
            }

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<ClienteReadDTO>> AddCliente([FromBody] ClienteCreateUpdateDTO cliente)
        {
         
            var agregarCliente = await _clienteService.Add(cliente);
            if (!agregarCliente.IsValid)
            {
                return BadRequest(agregarCliente.Errors);
            }
            return CreatedAtAction(nameof(GetCliente), new { id = agregarCliente.Data!.ClienteId }, agregarCliente.Data);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateCliente([FromBody] ClienteCreateUpdateDTO cliente, [FromRoute]int id)
        {
            var actualizarCliente = await _clienteService.Update(id,cliente);
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
