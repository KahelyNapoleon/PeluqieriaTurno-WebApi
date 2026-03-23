using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PeluqueriaTurnoWebApi.DTOs.HistorialTurnoDTOs;
using PeluqueriaTurnoWebApi.Mappings;


namespace PeluqueriaTurnoWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistorialTurnosController : ControllerBase
    {
       private readonly IHistorialTurnoService _historialTurnoService;

        public HistorialTurnosController(IHistorialTurnoService historialTurnoService)
        {
            _historialTurnoService = historialTurnoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialTurnoCreateUpdateDTO>>> GetHistorialTurno()
        {
            var historialTurnos = await _historialTurnoService.GetAll();
            if (!historialTurnos.IsValid)
            {
                return BadRequest(historialTurnos.Data);
            }

            var historialTurnosDto = historialTurnos.Data!.Select(h => h.ToDto());

            return Ok(historialTurnosDto);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<HistorialTurnoReadDTO>> GetHistorialTurno([FromQuery] int id)
        {
            var historialTurno = await _historialTurnoService.GetById(id);
            if (!historialTurno.IsValid)
            {
                return NotFound(historialTurno.Errors);
            }

            var historialTurnoDto = historialTurno.Data!.ToDto();

            return Ok(historialTurnoDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateHistorialTurno([FromBody] HistorialTurnoCreateUpdateDTO historialTurnoDto)
        {
            var historialTurno = historialTurnoDto.ToEntity();

            var crearHistorialTurno = await _historialTurnoService.Add(historialTurno);
            if (!crearHistorialTurno.IsValid)
            {
                return BadRequest(crearHistorialTurno.Errors);
            }

            return CreatedAtAction(nameof(GetHistorialTurno), new { crearHistorialTurno.Data!.HistorialTurnoId }, crearHistorialTurno.Data!);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateHistorialTurno([FromBody]HistorialTurnoCreateUpdateDTO historialTurnoDTO, [FromRoute] int id)
        {
            var historialTurno = historialTurnoDTO.ToEntity();
            var actualizarHistorialTurno = await _historialTurnoService.Update(id,historialTurno);
            if (!actualizarHistorialTurno.IsValid)
            {
                return BadRequest(actualizarHistorialTurno.Data);
            }

            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteHistorialTurno([FromBody] int id)
        {
            var eliminarHistorialTurno = await _historialTurnoService.Delete(id);
            if (!eliminarHistorialTurno.IsValid)
            {
                return BadRequest(eliminarHistorialTurno.Errors);
            }

            return NoContent();
        }
    }
}
