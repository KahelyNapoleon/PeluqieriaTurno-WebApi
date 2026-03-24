using BLL.Services.Interfaces;
using DomainLayer.Models;
using PeluqueriaTurnoWebApi.DTOs.PagoDTOs;
using Microsoft.AspNetCore.Mvc;
using PeluqueriaTurnoWebApi.Mappings;

namespace PeluqueriaTurnoWebApi.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class PagosController : ControllerBase
    {
        private readonly IPagoService _pagoService;

        public PagosController(IPagoService pagoService)
        {
            _pagoService = pagoService;          
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagoReadDTO>>> GetPagos()
        {
            var pagos = await _pagoService.GetAll();
            if (!pagos.IsValid)
            {
                return BadRequest(pagos.Errors);
            }

            var pagosDto = pagos.Data!.Select(p => p.ToReadDTO());
            return Ok(pagosDto);

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PagoReadDTO>> GetPago([FromRoute] int id)
        {
            var pago = await _pagoService.GetById(id);
            if (!pago.IsValid)
            {
                return NotFound(pago.Errors);
            }

            var pagoDto = pago.Data!.ToReadDTO();

            return Ok(pagoDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePago([FromForm] PagoCreateDTO pagoDto)
        {
            var pagoEntity = pagoDto.ToCreateEntity();
            var createPago = await _pagoService.Add(pagoEntity);
            if (!createPago.IsValid)
            {
                return BadRequest(createPago.Errors);
            }

            return CreatedAtAction(nameof(GetPago), new { id = createPago.Data!.PagoId}, createPago.Data);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdatePago([FromBody] PagoUpdateDTO pagoDto, [FromRoute] int id)
        {
            var pagoUpdateEntity = pagoDto.ToUpdateEntity();
            var actualizarPago = await _pagoService.Update(id ,pagoUpdateEntity);
            if (!actualizarPago.IsValid)
            {
                return BadRequest(actualizarPago.Errors);
            }

            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeletePago([FromRoute] int id)
        {
            var deletePago = await _pagoService.Delete(id);
            if (!deletePago.IsValid)
            {
                return BadRequest(deletePago);
            }

            return NoContent();
        }


    }
}
