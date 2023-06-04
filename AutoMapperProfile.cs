using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementTasks
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Todo, GetTodoDto>();
            CreateMap<addTodoDto, Todo>();
            CreateMap<UpdateTodoDto, Todo>();

        }
    }
}