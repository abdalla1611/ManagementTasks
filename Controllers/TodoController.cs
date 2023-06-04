using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ManagementTasks.Controllers
{
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
            return Ok(await this._TodoService.GetAll());
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
    }
}
