using BLL.Services.Interfaces;
using Contracts.DTOs.MetodoPagoDTOs;
using Microsoft.AspNetCore.Mvc;


namespace PeluqueriaTurnoWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetodoPagosController : ControllerBase
    {
        private readonly IMetodoPagoService _metodoPagoService;

        public MetodoPagosController(IMetodoPagoService metodoPagoService)
        {
            _metodoPagoService = metodoPagoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MetodoPagoReadDTO>>> GetMetodoPagos()
        {
            var metodoPagos = await _metodoPagoService.GetAll();
            if (!metodoPagos.IsValid)
            {
                return BadRequest(metodoPagos.Errors);
            }

            return Ok(metodoPagos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MetodoPagoReadDTO>> GetMetodoPago([FromRoute] int id)
        {
            var metodoPago = await _metodoPagoService.GetById(id);
            if (!metodoPago.IsValid)
            {
                return NotFound(metodoPago.Errors);
            }

            return Ok(metodoPago);
        }

        [HttpPost]
        public async Task<ActionResult> CreateMetodoPago([FromBody] MetodoPagoCreateUpdateDTO metodoPagoDto)
        {
            var crearMetodoPago = await _metodoPagoService.Add(metodoPagoDto);
            if (!crearMetodoPago.IsValid)
            {
                return BadRequest(crearMetodoPago.Errors);
            }

            return CreatedAtAction(nameof(GetMetodoPago), new { id = crearMetodoPago.Data!.MetodoPagoId }, crearMetodoPago.Data);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateMetodoPago([FromBody] MetodoPagoCreateUpdateDTO metodoPago, [FromRoute] int id)
        {
            var actualizarMetodoPago = await _metodoPagoService.Update(id, metodoPago);
            if (!actualizarMetodoPago.IsValid)
            {
                return BadRequest(actualizarMetodoPago.Errors);
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteMetodoPago([FromRoute] int id)
        {
            var eliminarMetodoPago = await _metodoPagoService.Delete(id);
            if (!eliminarMetodoPago.IsValid)
            {
                return BadRequest(eliminarMetodoPago.Errors);
            }

            return NoContent();
        }
    }
}
