using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ManagementTasks.Models;

namespace ManagementTasks.Services.TodoService
{
    public class TodoService : ITodoService
    {
        private readonly IMapper mapper;
        private readonly DataContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public TodoService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.context = context;
            this.mapper = mapper;

        }

        public async Task<ServiceResponse<List<GetTodoDto>>> AddTodo(addTodoDto newTodo)
        {
            var response = new ServiceResponse<List<GetTodoDto>>();
            var t = mapper.Map<Todo>(newTodo);
            var userId = GetUserId();
            t.user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            context.Todos.Add(t);
            await context.SaveChangesAsync();
            response.data = await context.Todos
            .Where(t => t.user!.Id == userId)
            .Select(t => mapper.Map<GetTodoDto>(t))
            .ToListAsync();
            return response;
        }

        public async Task<ServiceResponse<List<GetTodoDto>>> DeleteTodo(int id)
        {
            var userId = GetUserId();
            var response = new ServiceResponse<List<GetTodoDto>>();
            try
            {
                var todo = await context.Todos
                    .FirstOrDefaultAsync(t => t.Id == id && t.user!.Id == userId);

                if (todo is null)
                {
                    throw new Exception($"todo with id '{id}' not found");
                }
                context.Todos.Remove(todo);
                await context.SaveChangesAsync();
                response.data = context.Todos
                    .Where(t => t.user!.Id == userId)
                    .Select(t => mapper.Map<GetTodoDto>(t))
                    .ToList();

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
            var userId = GetUserId();
            var response = new ServiceResponse<List<GetTodoDto>>();
            var userTodos = await context.Todos.Where(t => t.user!.Id == userId).ToListAsync();
            response.data = userTodos.Select(t => mapper.Map<GetTodoDto>(t)).ToList();
            return response;
        }

        public async Task<ServiceResponse<List<GetTodoDto>>> FilterByDate(DateTime date)
        {
            var response = new ServiceResponse<List<GetTodoDto>>();
            var userId = GetUserId();
            var userTodos = await context.Todos
            .Where(t => t.user!.Id == userId && t.DueDate.Date == date.Date)
            .ToListAsync();

            response.data = userTodos.Select(t => mapper.Map<GetTodoDto>(t)).ToList();
            return response;
        }


        public async Task<ServiceResponse<List<GetTodoDto>>> GetByKeyword(string keyword)
        {
            var response = new ServiceResponse<List<GetTodoDto>>();
            var userId = GetUserId();
            var userTodos = await context.Todos
            .Where(t =>
            t.user!.Id == userId &&
            (t.Title.Contains(keyword) || t.description.Contains(keyword)))
            .ToListAsync();

            response.data = userTodos.Select(t => mapper.Map<GetTodoDto>(t)).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetTodoDto>> UpdateTodo(UpdateTodoDto updateTodo)
        {
            var response = new ServiceResponse<GetTodoDto>();
            try
            {
                var todo = await context.Todos
                .Include(t => t.user)
                .FirstOrDefaultAsync(t => t.Id == updateTodo.Id);
                if (todo is null || todo.user!.Id != GetUserId())
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

        private int GetUserId() => int.Parse(httpContextAccessor.HttpContext!.User.
            FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}

