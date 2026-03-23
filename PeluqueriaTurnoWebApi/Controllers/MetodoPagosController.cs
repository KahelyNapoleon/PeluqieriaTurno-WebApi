using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PeluqueriaTurnoWebApi.DTOs.MetodoPagoDTOs;
using PeluqueriaTurnoWebApi.Mappings;

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

            var metodosPagosDto = metodoPagos.Data!.Select(m => m.ToReadDTO());

            return Ok(metodosPagosDto);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MetodoPagoReadDTO>> GetMetodoPago([FromRoute] int id)
        {
            var metodoPago = await _metodoPagoService.GetById(id);
            if (!metodoPago.IsValid)
            {
                return NotFound(metodoPago.Errors);
            }

            var metodoPagoDto = metodoPago.Data!.ToReadDTO();

            return Ok(metodoPagoDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateMetodoPago([FromBody] MetodoPagoCreateUpdateDTO metodoPagoDto)
        {
            var metodoPagoEntity = metodoPagoDto.ToEntity();
            var crearMetodoPago = await _metodoPagoService.Add(metodoPagoEntity);
            if (!crearMetodoPago.IsValid)
            {
                return BadRequest(crearMetodoPago.Errors);
            }

            return CreatedAtAction(nameof(GetMetodoPago), new { id = crearMetodoPago.Data!.MetodoPagoId }, crearMetodoPago.Data);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateMetodoPago([FromBody] MetodoPagoCreateUpdateDTO metodoPago, [FromRoute] int id)
        {
            var metodoDatoEntity = metodoPago.ToEntity();
            var actualizarMetodoPago = await _metodoPagoService.Update(id, metodoDatoEntity);
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
