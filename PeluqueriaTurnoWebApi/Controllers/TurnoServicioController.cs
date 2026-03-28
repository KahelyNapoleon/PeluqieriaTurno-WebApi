using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PeluqueriaTurnoWebApi.DTOs.TurnoServicioDTOs;
using PeluqueriaTurnoWebApi.Mappings;

namespace PeluqueriaTurnoWebApi.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
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

            var turnoServiciosDtos = turnoServicios.Data!.Select(t => t.ToReadDto());
            return Ok(turnoServiciosDtos);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<TurnoServicioReadDTO>> GetTurnoServicios([FromRoute] int id)
        {
            var turnoServicios = await _turnoServicioService.GetById(id);
            if (!turnoServicios.IsValid)
            {
                return NotFound(turnoServicios.Errors);
            }

            var turnoServicioDto = turnoServicios.Data!.ToReadDto();
            return Ok(turnoServicioDto);
        }


        [HttpPost]
        public async Task<ActionResult> CreateTurnoServicio([FromBody] TurnoServicioCreateDTO turnoServicioDto)
        {
            var turnoServicioToEntity = turnoServicioDto.ToCreateEntity();
            var createTurnoServicio = await _turnoServicioService.Add(turnoServicioToEntity);
            if (!createTurnoServicio.IsValid)
            {
                return BadRequest(createTurnoServicio.Errors);
            }

            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateTurnoServicio([FromBody] TurnoServicioUpdateDTO turnoServicioDto, [FromRoute] int id)
        {
            var turnoServicioToEntity = turnoServicioDto.ToUpdateEntity();
            var updateTurnoServicio = await _turnoServicioService.Update(id,turnoServicioToEntity);
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
