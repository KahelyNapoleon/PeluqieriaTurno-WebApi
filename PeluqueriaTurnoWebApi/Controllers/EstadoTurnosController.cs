using BLL.Services;
using BLL.Services.Interfaces;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using PeluqueriaTurnoWebApi.DTOs.EstadoTurnoDTOs;
using PeluqueriaTurnoWebApi.Mappings;

namespace PeluqueriaTurnoWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadoTurnosController : ControllerBase
    {
        private readonly IEstadoTurnoService _estadoTurnoService;

        public EstadoTurnosController(IEstadoTurnoService estadoTurnoService)
        {
            _estadoTurnoService = estadoTurnoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoTurnoReadDTO>>> GetEstadoTurnos()
        {
            var estadoTurnos = await _estadoTurnoService.GetAll();
            if (!estadoTurnos.IsValid)
            {
                return BadRequest(estadoTurnos.Errors);
            }

            var estadoTurnosDTO = estadoTurnos.Data!.Select(e => e!.ToReadDTO());

            return Ok(estadoTurnosDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EstadoTurnoReadDTO>> GetEstadoTurno([FromRoute] int id)
        {
            var estadoTurno = await _estadoTurnoService.GetById(id);
            if (!estadoTurno.IsValid)
            {
                return BadRequest(estadoTurno.Errors);
            }

            var estadoTurnoDto = estadoTurno.Data!.ToReadDTO();

            return Ok(estadoTurnoDto);
        }

        [HttpPost]
        public async Task<ActionResult<EstadoTurnoReadDTO>> CreateEstadoTurno([FromBody] EstadoTurnoCreateUpdateDTO estadoTurno)
        {
            var estadoTurnoToEntity = estadoTurno.ToEntity();

            var crearEstadoTurno = await _estadoTurnoService.Add(estadoTurnoToEntity);
            if (!crearEstadoTurno.IsValid)
            {
                return BadRequest(crearEstadoTurno.Errors);
            }

            return CreatedAtAction(nameof(GetEstadoTurno), new { id = crearEstadoTurno.Data!.EstadoTurnoId}, estadoTurno);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateEstadoTurno([FromBody] EstadoTurnoCreateUpdateDTO estadoTurnoDto, [FromRoute] int id)
        {
            //EstadoTurno estadoTurnoEntity = new EstadoTurno();
            //estadoTurnoDto.ToUpdate(estadoTurnoEntity);
            var estadoTurnoToEntity = estadoTurnoDto.ToEntity();

            var actualizarEstadoTurno = await _estadoTurnoService.Update(id,estadoTurnoToEntity);
            if (!actualizarEstadoTurno.IsValid)
            {
                return BadRequest(actualizarEstadoTurno.Errors);
            }

            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteEstadoTurno([FromRoute] int id)
        {
            var eliminarEstadoTurno = await _estadoTurnoService.Delete(id);
            if (!eliminarEstadoTurno.IsValid)
            {
                return NotFound(eliminarEstadoTurno.Errors);
            }

            return NoContent();
        }
    }
}
