using BLL.Services.Interfaces;
using DAL.Repositorios.Interfaces;
using DomainLayer.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PeluqueriaTurnoWebApi.DTOs.ClienteDTOs;

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
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            var clientes = await _clienteService.GetAll();
            if (!clientes.IsValid)
            {
                return NotFound(clientes.Errors);
            }
            return Ok(clientes.Data);
        }

        [HttpGet("/cliente/{id:int}")]
        public async Task<ActionResult<Cliente>> GetCliente([FromRoute] int id)
        {
            var cliente = await _clienteService.GetById(id);
            if(!cliente.IsValid)
            {
                return BadRequest(cliente.Errors);
            }

            return Ok(cliente.Data);
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> AddCliente([FromBody] Cliente cliente)
        {
            var agregarCliente = await _clienteService.Add(cliente);
            if (!agregarCliente.IsValid)
            {
                return BadRequest(agregarCliente.Errors);
            }

            return CreatedAtAction(nameof(GetCliente), new { id = agregarCliente.Data!.ClienteId }, agregarCliente.Data);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateCliente([FromBody] ClienteUpdateDTO cliente, [FromRoute]int id)
        {
            var clienteUpdate = new Cliente
            {
                ClienteId = id,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                NroCelular = cliente.NroCelular,
                CorreoElectronico = cliente.CorreoElectronico,
                FechaNacimiento = cliente.FechaNacimiento,
                Preferencias = cliente.Preferencias,
                Observaciones = cliente.Observaciones,
                Activo = cliente.Activo
            };

            var actualzarCliente = await _clienteService.Update(id,clienteUpdate);
            if (!actualzarCliente.IsValid)
            {
                return BadRequest(actualzarCliente.Errors);
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
