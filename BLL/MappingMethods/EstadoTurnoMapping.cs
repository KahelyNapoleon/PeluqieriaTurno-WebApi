using BLL.Mapping;
using Contracts.DTOs.EstadoTurnoDTOs;
using DomainLayer.Models;


namespace BLL.MappingMethods
{
    public class EstadoTurnoMapping : IMappingService<EstadoTurno, EstadoTurnoReadDTO, EstadoTurnoCreateUpdateDTO>
    {
        public EstadoTurno ToEntity(EstadoTurnoCreateUpdateDTO et)
        {
            return new EstadoTurno
            {
                Descripcion = et.Descripcion
            };
        }

        public void UpdateEntity(EstadoTurnoCreateUpdateDTO estadoTurnoDto, EstadoTurno estadoTurno)
        {
            estadoTurno.Descripcion = estadoTurnoDto.Descripcion;
        }

        public EstadoTurnoReadDTO ToReadDTO(EstadoTurno e)
        {
            return new EstadoTurnoReadDTO
            {
                EstadoTurnoId = e.EstadoTurnoId,
                Descripcion = e.Descripcion
            };
        }
               
            
        
    }
}
