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
        public ActionResult<List<Todo>> Get()
        {
            return Ok(this._TodoService.GetAll());
        }
        [HttpGet("{Id}")]
        public ActionResult<Todo> GetSingle(int Id)
        {
            return Ok(this._TodoService.GetById(Id));
        }

        [HttpPost("")]
        public ActionResult<List<Todo>> AddTodo(Todo newTodo)
        {
            return Ok(this._TodoService.AddTodo(newTodo));
        }
    }
}
