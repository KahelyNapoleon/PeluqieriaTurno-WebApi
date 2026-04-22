using BLL.Services.Interfaces;
using Contracts.DTOs.TurnoDTOs;
using Microsoft.AspNetCore.Mvc;


namespace PeluqueriaTurnoWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurnosController : Controller
    {
        private readonly ITurnoService _turnoService;

        public TurnosController(ITurnoService turnoService)
        {
            _turnoService = turnoService;
        }

        [HttpGet("paged")]
        public async Task<ActionResult<TurnoReadDTO>> GetPaged([FromQuery] int pageNumber, [FromQuery] int pageSize = 10)
        {
            var turnos = await _turnoService.GetPaged(pageNumber, pageSize);
            if (!turnos.IsValid)
            {
                return BadRequest(turnos.Errors);
            }

            return Ok(turnos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TurnoReadDTO>> GetTurno([FromRoute] int id)
        {
            var turno = await _turnoService.GetById(id);
            if (!turno.IsValid)
            {
                return NotFound(turno.Errors);
            }

            return Ok(turno);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTurno([FromBody] TurnoCreateUpdateDTO turno)
        {
            var agregarTurno = await _turnoService.Add(turno);
            if (!agregarTurno.IsValid)
            {
                return BadRequest(agregarTurno.Errors);
            }

            return CreatedAtAction(nameof(GetTurno), new { id = agregarTurno.Data!.TurnoId}, agregarTurno.Data);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateTurno([FromBody] TurnoCreateUpdateDTO turno, [FromRoute] int id)
        {
            var updateTurno = await _turnoService.Update(id, turno);
            if (!updateTurno.IsValid)
            {
                return BadRequest(updateTurno.Errors);
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteTurno([FromRoute] int id)
        {
            var removeTurno = await _turnoService.Delete(id);
            if (!removeTurno.IsValid)
            {
                return NotFound(removeTurno.Errors);
            }

            return NoContent();
        }
    }
}
