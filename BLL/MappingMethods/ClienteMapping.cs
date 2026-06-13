using DomainLayer.Models;
using Contracts.DTOs.ClienteDTOs;
using BLL.Mapping;

namespace BLL.MappingMethods
{
    public class ClienteMapping : IMappingService<Cliente,ClienteReadDTO,ClienteCreateUpdateDTO>
    {
        public Cliente ToEntity(ClienteCreateUpdateDTO clienteCreateDto)
        {
            return new Cliente
            {
                Nombre = clienteCreateDto.Nombre,
                Apellido = clienteCreateDto.Apellido,
                NroCelular = clienteCreateDto.NroCelular,
                CorreoElectronico = clienteCreateDto.CorreoElectronico,
                FechaNacimiento = clienteCreateDto.FechaNacimiento,
                Preferencias = clienteCreateDto.Preferencias,
                Observaciones = clienteCreateDto.Observaciones,
            };
        }

        public void UpdateEntity(ClienteCreateUpdateDTO dto, Cliente c)
        {
            c.Nombre = dto.Nombre;
            c.Apellido = dto.Apellido;
            c.NroCelular= dto.NroCelular;
            c.CorreoElectronico= dto.CorreoElectronico;
            c.FechaNacimiento = dto.FechaNacimiento;
            c.Preferencias = dto.Preferencias;
            c.Observaciones = dto.Observaciones;
        }

        public ClienteReadDTO ToReadDTO(Cliente c)
        {
            return new ClienteReadDTO
            {
                ClienteId = c.ClienteId,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                NroCelular = c.NroCelular,
                CorreoElectronico = c.CorreoElectronico,
                FechaNacimiento = c.FechaNacimiento,
                Preferencias = c.Preferencias,
                Observaciones = c.Observaciones,
                Activo = c.Activo
            };
        }

       
    }
}
