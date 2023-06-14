using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementTasks.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class TodoController : ControllerBase
    {
        private readonly ITodoService _TodoService;
        public TodoController(ITodoService todoService)
        {
            this._TodoService = todoService;

        }

        [HttpGet()]
        public async Task<ActionResult<ServiceResponse<List<GetTodoDto>>>> Get()
        {
            return Ok(await this._TodoService.GetAllTodos());
        }
        [HttpGet("Search")]
        public async Task<ActionResult<ServiceResponse<List<GetTodoDto>>>> GetByKeyword(string keyword)
        {
            return Ok(await this._TodoService.GetByKeyword(keyword));
        }

        [HttpGet("Filter")]
        public async Task<ActionResult<ServiceResponse<List<GetTodoDto>>>> FilterByDate(DateTime date)
        {
            return Ok(await this._TodoService.FilterByDate(date));
        }

        [HttpPost("")]
        public async Task<ActionResult<ServiceResponse<object>>> AddTodo(addTodoDto newTodo)
        {
            return Ok(await this._TodoService.AddTodo(newTodo));
        }

        [HttpPut("")]
        public async Task<ActionResult<ServiceResponse<object>>> UpdateTodo(UpdateTodoDto updateTodo)
        {
            var response = await this._TodoService.UpdateTodo(updateTodo);
            if (response.success)
            {
                return Ok(response);
            }
            return NotFound(response);

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<ServiceResponse<object>>> DeleteTodo(int Id)
        {
            var response = await this._TodoService.DeleteTodo(Id);
            if (response.success)
            {
                return Ok(response);
            }
            return NotFound(response);
        }
    }
}
