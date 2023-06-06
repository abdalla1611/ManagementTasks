using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagementTasks.Models;

namespace ManagementTasks.Services.TodoService
{
    public class TodoService : ITodoService
    {
        private readonly IMapper mapper;
        private readonly DataContext context;

        public TodoService(IMapper mapper, DataContext context)
        {
            this.context = context;
            this.mapper = mapper;

        }

        public async Task<ServiceResponse<List<GetTodoDto>>> AddTodo(addTodoDto newTodo)
        {
            var response = new ServiceResponse<List<GetTodoDto>>();
            var t = mapper.Map<Todo>(newTodo);
            context.Todos.Add(t);
            await context.SaveChangesAsync();
            response.data = await context.Todos.Select(t => mapper.Map<GetTodoDto>(t)).ToListAsync();
            return response;
        }

        public async Task<ServiceResponse<List<GetTodoDto>>> DeleteTodo(int id)
        {
            var response = new ServiceResponse<List<GetTodoDto>>();
            try
            {
                var todo = await context.Todos.FirstOrDefaultAsync(t => t.Id == id);
                Console.WriteLine(todo);
                if (todo is null)
                {
                    throw new Exception($"todo with id '{id}' not found");
                }
                context.Todos.Remove(todo);
                await context.SaveChangesAsync();
                response.data = context.Todos.Select(t => mapper.Map<GetTodoDto>(t)).ToList();
            }
            catch (Exception e)
            {
                response.success = false;
                response.message = e.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetTodoDto>>> GetAllTodos()
        {
            var response = new ServiceResponse<List<GetTodoDto>>();
            var todos = await context.Todos.ToListAsync();
            response.data = todos.Select(t => mapper.Map<GetTodoDto>(t)).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetTodoDto>> GetById(int id)
        {
            var response = new ServiceResponse<GetTodoDto>();

            var todo = await context.Todos.FirstOrDefaultAsync(t => t.Id == id);
            response.data = mapper.Map<GetTodoDto>(todo);
            return response;
        }

        public async Task<ServiceResponse<GetTodoDto>> UpdateTodo(UpdateTodoDto updateTodo)
        {
            var response = new ServiceResponse<GetTodoDto>();
            try
            {
                var todo = await context.Todos.FirstOrDefaultAsync(t => t.Id == updateTodo.Id);
                if (todo is null)
                {
                    throw new Exception($"todo with id '{updateTodo.Id}' not found");
                }
                mapper.Map(updateTodo, todo);
                await context.SaveChangesAsync();

                response.data = mapper.Map<GetTodoDto>(todo);
            }
            catch (Exception e)
            {
                response.success = false;
                response.message = e.Message;
            }


            return response;
        }
    }
}

