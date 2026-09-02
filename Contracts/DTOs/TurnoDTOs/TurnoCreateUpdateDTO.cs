using Contracts.DTOs.ServicioDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTOs.TurnoDTOs
{
    public class TurnoCreateUpdateDTO
    {
        public string? Detalle { get; set; }//EL DETALLE PUEDE CAMBIAR SI EL CLIENTE CAMBIA O QUITA UN SERVICIO.

        public int ClienteId { get; set; } // Se debe dejar este campo para actualizar, en caso de que el cliente
                                           //de baja el turno y el msimo quede libre
        public int EstadoTurnoId { get; set; } //SI SE ACTUALIZA AL CONFIRMAR
                                               //RECORDAR ESTAOD: LIBRE>CONFIRMAR>OCUPADO>EN PROCESO>FINALIZADO
        public TimeOnly HoraTurno { get; set; }

        public DateOnly FechaTurno { get; set; }

        public List<int> ServiciosId { get; set; } = new List<int>();
      
    }
}
