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
        public List<Todo> AddTodo(Todo newTodo)
        {
            todos.Add(newTodo);
            return todos;
        }

        public List<Todo> GetAll()
        {
            return todos;
        }

        public Todo GetById(int id)
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);
            if (todo is not null)
            {
                return todo;
            }
            throw new Exception("task not found");
        }
    }
}