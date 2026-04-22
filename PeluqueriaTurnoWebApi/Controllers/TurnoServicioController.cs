using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Contracts.DTOs.TurnoServicioDTOs;

namespace PeluqueriaTurnoWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurnoServicioController : ControllerBase
    {
       private readonly ITurnoServicioService _turnoServicioService;

        public TurnoServicioController(ITurnoServicioService turnoServicio)
        {
            _turnoServicioService = turnoServicio;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TurnoServicioReadDTO>>> GetTurnoServicios()
        {
            var turnoServicios = await _turnoServicioService.GetAll();
            if (!turnoServicios.IsValid)
            {
                return BadRequest(turnoServicios.Errors);
            }

      
            return Ok(turnoServicios);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<TurnoServicioReadDTO>> GetTurnoServicio([FromRoute] int id)
        {
            var turnoServicios = await _turnoServicioService.GetById(id);
            if (!turnoServicios.IsValid)
            {
                return NotFound(turnoServicios.Errors);
            }

            return Ok(turnoServicios);
        }


        [HttpPost]
        public async Task<ActionResult<TurnoServicioReadDTO>> CreateTurnoServicio([FromBody] TurnoServicioCreateUpdateDTO turnoServicioDto)
        {
            var createTurnoServicio = await _turnoServicioService.Add(turnoServicioDto);
            if (!createTurnoServicio.IsValid)
            {
                return BadRequest(createTurnoServicio.Errors);
            }

            return CreatedAtAction(nameof(GetTurnoServicio), new { id = createTurnoServicio.Data!.TurnoServicioId}, createTurnoServicio.Data);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateTurnoServicio([FromBody] TurnoServicioCreateUpdateDTO turnoServicioDto, [FromRoute] int id)
        {
            var updateTurnoServicio = await _turnoServicioService.Update(id,turnoServicioDto);
            if (!updateTurnoServicio.IsValid)
            {
                return BadRequest(updateTurnoServicio.Errors);
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteTurnoServicio([FromRoute]int id)
        {
            var eliminarTurnoServicio = await _turnoServicioService.Delete(id);
            if (!eliminarTurnoServicio.IsValid)
            {
                return NotFound(eliminarTurnoServicio.Errors);
            }

            return NoContent();
        }
    }
}
