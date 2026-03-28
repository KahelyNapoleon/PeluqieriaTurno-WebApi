using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
     
    }
}
