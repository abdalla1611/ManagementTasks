using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagementTasks.Models;

namespace ManagementTasks.Services.TodoService
{
    public class TodoService : ITodoService
    {

        private static List<Todo> todos = new List<Todo>{
            new Todo(),
            new Todo {Id = 1}
        };
        private readonly IMapper mapper;

        public TodoService(IMapper mapper)
        {
            this.mapper = mapper;

        }

        public async Task<ServiceResponse<List<GetTodoDto>>> AddTodo(addTodoDto newTodo)
        {
            var response = new ServiceResponse<List<GetTodoDto>>();
            var t = mapper.Map<Todo>(newTodo);
            t.Id = todos.Max(t => t.Id) + 1;
            todos.Add(t);
            response.data = todos.Select(t => mapper.Map<GetTodoDto>(t)).ToList();
            return response;
        }

        public async Task<ServiceResponse<List<GetTodoDto>>> GetAll()
        {
            var response = new ServiceResponse<List<GetTodoDto>>();
            response.data = todos.Select(t => mapper.Map<GetTodoDto>(t)).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetTodoDto>> GetById(int id)
        {
            var response = new ServiceResponse<GetTodoDto>();
            var todo = todos.FirstOrDefault(t => t.Id == id);
            response.data = mapper.Map<GetTodoDto>(todo);
            return response;
        }
    }
}

