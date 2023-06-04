using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ManagementTasks.Services.TodoService
{
    public interface ITodoService
    {
        Task<ServiceResponse<List<GetTodoDto>>> GetAll();
        Task<ServiceResponse<GetTodoDto>> GetById(int id);
        Task<ServiceResponse<List<GetTodoDto>>> AddTodo(addTodoDto newTodo);
    }
}