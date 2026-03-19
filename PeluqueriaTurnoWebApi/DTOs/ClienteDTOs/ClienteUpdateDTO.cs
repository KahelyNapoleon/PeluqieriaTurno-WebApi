using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeluqueriaTurnoWebApi.DTOs.ClienteDTOs
{
    public class ClienteUpdateDTO
    {
        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string NroCelular { get; set; } = null!;

        public string? CorreoElectronico { get; set; }

        public DateOnly? FechaNacimiento { get; set; }

        public string? Preferencias { get; set; }

        public string? Observaciones { get; set; }

        public bool Activo { get; set; }
    }
}
