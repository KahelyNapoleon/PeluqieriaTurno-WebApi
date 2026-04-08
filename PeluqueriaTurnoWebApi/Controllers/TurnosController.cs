using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PeluqueriaTurnoWebApi.DTOs.TurnoDTOs;
using PeluqueriaTurnoWebApi.Mappings;

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

        [HttpGet]
        public async Task<ActionResult<TurnoReadDTO>> GetPaged([FromQuery] int pageNumber, [FromQuery] int pageSize = 10)
        {
            var turnos = await _turnoService.GetPaged(pageNumber, pageSize);
            if (!turnos.IsValid)
            {
                return BadRequest(turnos.Errors);
            }

            //Transormar turno a TurnoReadDTO.
            var turnosToDto = turnos.Data!.Select(t => t!.ToDTO());

            return Ok(turnosToDto);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TurnoReadDTO>> GetTurno([FromRoute] int id)
        {
            var turno = await _turnoService.GetById(id);
            if (!turno.IsValid)
            {
                return NotFound(turno.Errors);
            }

            var turnoToDto = turno.Data!.ToDTO();
            return Ok(turnoToDto);
        } 
     
    }
}
