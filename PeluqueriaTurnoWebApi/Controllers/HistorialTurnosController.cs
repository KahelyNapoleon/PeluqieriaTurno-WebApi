using BLL.Services.Interfaces;
using Contracts.DTOs.HistorialTurnoDTOs;
using Microsoft.AspNetCore.Mvc;


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

            return Ok(historialTurnos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<HistorialTurnoReadDTO>> GetHistorialTurno([FromQuery] int id)
        {
            var historialTurno = await _historialTurnoService.GetById(id);
            if (!historialTurno.IsValid)
            {
                return NotFound(historialTurno.Errors);
            }

            return Ok(historialTurno);
        }

        [HttpPost]
        public async Task<ActionResult> CreateHistorialTurno([FromBody] HistorialTurnoCreateUpdateDTO historialTurnoDto)
        {
            var crearHistorialTurno = await _historialTurnoService.Add(historialTurnoDto);
            if (!crearHistorialTurno.IsValid)
            {
                return BadRequest(crearHistorialTurno.Errors);
            }

            return CreatedAtAction(nameof(GetHistorialTurno), new { crearHistorialTurno.Data!.HistorialTurnoId }, crearHistorialTurno.Data!);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateHistorialTurno([FromBody]HistorialTurnoCreateUpdateDTO historialTurnoDTO, [FromRoute] int id)
        {
           
            var actualizarHistorialTurno = await _historialTurnoService.Update(id,historialTurnoDTO);
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
