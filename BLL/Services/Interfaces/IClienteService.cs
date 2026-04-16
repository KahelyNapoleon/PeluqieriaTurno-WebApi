using Contracts.DTOs.ClienteDTOs;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IClienteService : IGenericService<Cliente, ClienteReadDTO, ClienteCreateUpdateDTO>
    {
    }
}
