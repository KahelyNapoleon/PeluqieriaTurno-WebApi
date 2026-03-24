using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PeluqueriaTurnoWebApi.DTOs.TipoServicioDTOs;
using PeluqueriaTurnoWebApi.Mappings;

namespace PeluqueriaTurnoWebApi.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class TipoServicioController : ControllerBase
    {
        private readonly ITipoServicioService _tipoServicio;

        public TipoServicioController(ITipoServicioService tipoServicio)
        {
            _tipoServicio = tipoServicio;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoServicioReadDTO>>> GetTipoServicios()
        {
            var tipoServicios = await _tipoServicio.GetAll();
            if (!tipoServicios.IsValid)
            {
                return BadRequest(tipoServicios.Errors);
            }

            var tipoServiciosDto = tipoServicios.Data!.Select(t => t.ToReadDto());
            return Ok(tipoServiciosDto);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoServicioReadDTO>> GetTipoServicio([FromRoute]int id)
        {
            var tipoServicio = await _tipoServicio.GetById(id);
            if (!tipoServicio.IsValid)
            {
                return NotFound(tipoServicio.Errors);
            }

            var tipoServicioDto = tipoServicio.Data!.ToReadDto();
            return Ok(tipoServicioDto);
        }

        [HttpPost]
        public async Task<ActionResult> GetTipoServicio([FromBody] TipoServicioCreateUpdateDTO tipoServicioDto)
        {
            var tipoServicioToEntity = tipoServicioDto.ToEntity();
            var crearTipoServicio = await _tipoServicio.Add(tipoServicioToEntity);
            if (!crearTipoServicio.IsValid)
            {
                return BadRequest(crearTipoServicio.Errors);
            }

            return CreatedAtAction(nameof(GetTipoServicio), new { id = crearTipoServicio.Data!.TipoServicioId}, crearTipoServicio.Data);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateTipoServicio([FromBody] TipoServicioCreateUpdateDTO tipoServicioDto, [FromRoute]int id)
        {
            var tipoServicioToEntity = tipoServicioDto.ToEntity();
            var actualizarTipoServicio = await _tipoServicio.Update(id,tipoServicioToEntity);
            if (!actualizarTipoServicio.IsValid)
            {
                return BadRequest(actualizarTipoServicio.Errors);
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteTipoServicio([FromRoute] int id)
        {
            var eliminarTipoServicio = await _tipoServicio.Delete(id);
            if (!eliminarTipoServicio.IsValid)
            {
                return NotFound(eliminarTipoServicio.Errors);
            }

            return NoContent();
        }


    }
}
