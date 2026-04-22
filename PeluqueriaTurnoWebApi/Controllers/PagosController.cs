using BLL.Services.Interfaces;
using Contracts.DTOs.PagoDTOs;
using Microsoft.AspNetCore.Mvc;


namespace PeluqueriaTurnoWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

            return Ok(pagos);

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PagoReadDTO>> GetPago([FromRoute] int id)
        {
            var pago = await _pagoService.GetById(id);
            if (!pago.IsValid)
            {
                return NotFound(pago.Errors);
            }

            return Ok(pago);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePago([FromForm] PagoCreateUpdateDTO pagoDto)
        {
            var createPago = await _pagoService.Add(pagoDto);

            if (!createPago.IsValid)
            {
                return BadRequest(createPago.Errors);
            }

            return CreatedAtAction(nameof(GetPago), new { id = createPago.Data!.PagoId}, createPago.Data);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdatePago([FromBody] PagoCreateUpdateDTO pagoDto, [FromRoute] int id)
        {
         
            var actualizarPago = await _pagoService.Update(id ,pagoDto);
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
