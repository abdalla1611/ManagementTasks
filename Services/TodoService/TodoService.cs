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


        public async Task<ServiceResponse<List<Todo>>> AddTodo(Todo newTodo)
        {
            var response = new ServiceResponse<List<Todo>>();
            todos.Add(newTodo);
            response.data = todos;
            return response;
        }

        public async Task<ServiceResponse<List<Todo>>> GetAll()
        {
            var response = new ServiceResponse<List<Todo>>();
            response.data = todos;
            return response;
        }

        public async Task<ServiceResponse<Todo>> GetById(int id)
        {
            var response = new ServiceResponse<Todo>();
            var todo = todos.FirstOrDefault(t => t.Id == id);
            response.data = todo;
            return response;
        }
    }
}

