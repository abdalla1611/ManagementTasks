using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetTodoDto>>>> Get()
        {
            return Ok(await this._TodoService.GetAllTodos());
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ServiceResponse<GetTodoDto>>> GetSingle(int Id)
        {
            return Ok(await this._TodoService.GetById(Id));
        }

        [HttpPost("")]
        public async Task<ActionResult<ServiceResponse<List<GetTodoDto>>>> AddTodo(addTodoDto newTodo)
        {
            return Ok(await this._TodoService.AddTodo(newTodo));
        }

        [HttpPut("")]
        public async Task<ActionResult<ServiceResponse<GetTodoDto>>> UpdateTodo(UpdateTodoDto updateTodo)
        {
            var response = await this._TodoService.UpdateTodo(updateTodo);
            if (response.success)
            {
                return Ok(response);
            }
            return NotFound(response);

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<ServiceResponse<GetTodoDto>>> DeleteTodo(int Id)
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
