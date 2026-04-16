using Contracts.DTOs.PagoDTOs;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IPagoService : IGenericService<Pago, PagoReadDTO, PagoCreateUpdateDTO>
    {
    }
}
