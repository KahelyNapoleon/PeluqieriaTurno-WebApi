using DomainLayer.Models;
using PeluqueriaTurnoWebApi.DTOs.EstadoTurnoDTOs;

namespace PeluqueriaTurnoWebApi.Mappings
{
    internal static class EstadoTurnoMapping
    {
        public static EstadoTurno ToEntity(this EstadoTurnoCreateUpdateDTO et)
        => new EstadoTurno
        {
            Descripcion = et.Descripcion
        };

        public static void ToUpdate(this EstadoTurnoCreateUpdateDTO estadoTurnoDto, EstadoTurno estadoTurno)
        {
            estadoTurno.Descripcion = estadoTurnoDto.Descripcion;
        }

        public static EstadoTurnoCreateUpdateDTO ToDto(this EstadoTurno estadpTurno)
        => new EstadoTurnoCreateUpdateDTO
        {
            Descripcion = estadpTurno.Descripcion
        };

        public static EstadoTurnoReadDTO ToReadDTO(this EstadoTurno e)
        {
            return new EstadoTurnoReadDTO
            {
                EstadoTurnoId = e.EstadoTurnoId,
                Descripcion = e.Descripcion
            };
        }
               
            
        
    }
}
