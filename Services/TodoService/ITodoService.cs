using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagementTasks.Models;

namespace ManagementTasks.Services.TodoService
{
    public interface ITodoService
    {
        Task<ServiceResponse<List<Todo>>> GetAll();
        Task<ServiceResponse<Todo>> GetById(int id);

        Task<ServiceResponse<List<Todo>>> AddTodo(Todo newTodo);
    }
}