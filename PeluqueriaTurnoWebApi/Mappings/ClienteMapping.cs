using DomainLayer.Models;
using PeluqueriaTurnoWebApi.DTOs.ClienteDTOs;

namespace PeluqueriaTurnoWebApi.Mappings
{
    internal static class ClienteMapping
    {
        public static Cliente ToEntity(this ClienteCreateDTO clienteCreateDto)
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

        public static void UpdateCliente(this ClienteUpdateDTO clienteUpdateDto, Cliente c)
        {
            c.Nombre = clienteUpdateDto.Nombre;
            c.Apellido = clienteUpdateDto.Apellido;
            c.NroCelular = clienteUpdateDto.NroCelular;
            c.CorreoElectronico = clienteUpdateDto.CorreoElectronico;
            c.FechaNacimiento = clienteUpdateDto.FechaNacimiento;
            c.Preferencias = clienteUpdateDto.Preferencias;
            c.Observaciones = clienteUpdateDto.Observaciones;
            c.Activo = clienteUpdateDto.Activo;
        }

        public static ClienteCreateDTO ToDTO(this Cliente c)
        {
            return new ClienteCreateDTO
            {
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

        public static ClienteReadDTO ToReadDTO(this Cliente c)
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
