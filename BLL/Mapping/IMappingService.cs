using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public interface IMappingService<Entity,ReadDto,CreateUpdateDto> where Entity : class 
                                                 where ReadDto : class
                                                 where CreateUpdateDto : class
    {
        Entity ToEntity(CreateUpdateDto dto);
        void UpdateEntity(CreateUpdateDto dto, Entity entity);
        ReadDto ToReadDTO(Entity entity);       
        
    }
}
