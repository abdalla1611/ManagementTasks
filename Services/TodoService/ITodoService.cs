using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ManagementTasks.Services.TodoService
{
    public interface ITodoService
    {
        Task<ServiceResponse<List<GetTodoDto>>> GetAllTodos();
        Task<ServiceResponse<List<GetTodoDto>>> FilterByDate(DateTime date);
        Task<ServiceResponse<List<GetTodoDto>>> GetByKeyword(string keyword);
        Task<ServiceResponse<object>> AddTodo(addTodoDto newTodo);
        Task<ServiceResponse<object>> UpdateTodo(UpdateTodoDto updateTodo);
        Task<ServiceResponse<object>> DeleteTodo(int id);


    }
}