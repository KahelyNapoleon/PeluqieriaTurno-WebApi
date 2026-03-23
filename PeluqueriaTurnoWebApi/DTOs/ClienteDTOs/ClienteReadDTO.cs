namespace PeluqueriaTurnoWebApi.DTOs.ClienteDTOs
{
    public class ClienteReadDTO
    {
        public int ClienteId { get; set; }

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string NroCelular { get; set; } = null!;

        public string? CorreoElectronico { get; set; }

        public DateOnly? FechaNacimiento { get; set; }

        public string? Preferencias { get; set; }

        public string? Observaciones { get; set; }

        public bool Activo { get; set; } = true;
    }
}
