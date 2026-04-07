using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PeluqueriaTurnoWebApi.DTOs.TurnoDTOs;

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

        [HttpGet("{pageNumber:int}")]
        public async Task<ActionResult<TurnoReadDTO>> GetPage([FromRoute] int pageNumber)
        {
           // var turnos = await _turnoService.GetPage // Falta agregar este metodo a turnoServicio!!
        }
     
    }
}
