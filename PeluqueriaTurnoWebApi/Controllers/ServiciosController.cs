using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PeluqueriaTurnoWebApi.DTOs.ServicioDTOs;
using PeluqueriaTurnoWebApi.Mappings;

namespace PeluqueriaTurnoWebApi.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class ServiciosController : ControllerBase
    {
        private readonly IServicioService _servicioService;

        public ServiciosController(IServicioService servicioService)
        {
            _servicioService = servicioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServicioReadDTO>>> GetServicios()
        {
            var servicios = await _servicioService.GetAll();
            if (!servicios.IsValid)
            {
                return BadRequest(servicios.Errors);
            }

            var serviciosDto = servicios.Data!.Select(s => s.ToReadDto());
            return Ok(serviciosDto);

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServicioReadDTO>> GetServicio([FromRoute] int id)
        {
            var servicio = await _servicioService.GetById(id);
            if (!servicio.IsValid)
            {
                return NotFound(servicio.Errors);
            }

            var servicioDto = servicio.Data!.ToReadDto();
            return Ok(servicioDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateServicio([FromBody] ServicioCreateUpdateDTO servicioDto)
        {
            var servicioEntity = servicioDto.ToEntity();
            var createServicio = await _servicioService.Add(servicioEntity);
            if (!createServicio.IsValid)
            {
                return BadRequest(createServicio.Errors);
            }

            return CreatedAtAction(nameof(GetServicio), new { id = createServicio.Data!.ServicioId }, createServicio.Data);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateServicio([FromBody] ServicioCreateUpdateDTO servicioDto, [FromRoute] int id)
        {
            var servicioToEntity = servicioDto.ToEntity();
            var actualizarServicio = await _servicioService.Update(id, servicioToEntity);
            if (!actualizarServicio.IsValid)
            {
                return BadRequest(actualizarServicio.Errors);
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteServicio([FromRoute] int id)
        {
            var deleteServicio = await _servicioService.Delete(id);
            if (!deleteServicio.IsValid)
            {
                return NotFound(deleteServicio.Errors);
            }

            return NoContent();
        }
    }
}
