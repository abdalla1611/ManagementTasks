using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagementTasks.Models;

namespace ManagementTasks.Services.TodoService
{
    public interface ITodoService
    {
        List<Todo> GetAll();
        Todo GetById(int id);

        List<Todo> AddTodo(Todo newTodo);
    }
}