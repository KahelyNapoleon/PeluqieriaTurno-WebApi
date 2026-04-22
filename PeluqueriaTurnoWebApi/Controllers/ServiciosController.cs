using BLL.Services.Interfaces;
using Contracts.DTOs.ServicioDTOs;
using Microsoft.AspNetCore.Mvc;

namespace PeluqueriaTurnoWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

            return Ok(servicios);

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServicioReadDTO>> GetServicio([FromRoute] int id)
        {
            var servicio = await _servicioService.GetById(id);
            if (!servicio.IsValid)
            {
                return NotFound(servicio.Errors);
            }

            return Ok(servicio);
        }

        [HttpPost]
        public async Task<ActionResult<ServicioReadDTO>> CreateServicio([FromBody] ServicioCreateUpdateDTO servicioDto)
        {
            var createServicio = await _servicioService.Add(servicioDto);
            if (!createServicio.IsValid)
            {
                return BadRequest(createServicio.Errors);
            }

            return CreatedAtAction(nameof(GetServicio), new { id = createServicio.Data!.ServicioId }, createServicio.Data);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateServicio([FromBody] ServicioCreateUpdateDTO servicioDto, [FromRoute] int id)
        {
            var actualizarServicio = await _servicioService.Update(id, servicioDto);
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
