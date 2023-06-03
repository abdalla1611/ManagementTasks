using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagementTasks.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManagementTasks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private static List<Todo> todos = new List<Todo>{
            new Todo(),
            new Todo {Id = 1}
        };

        [HttpGet("GetAll")]
        public ActionResult<List<Todo>> Get()
        {
            return Ok(todos);
        }
        [HttpGet("{Id}")]
        public ActionResult<Todo> GetSingle(int Id)
        {
            return Ok(todos.FirstOrDefault(t => t.Id == Id));
        }

        [HttpPost("")]
        public ActionResult<List<Todo>> AddTodo(Todo newTodo)
        {
            todos.Add(newTodo);
            return Ok(todos);
        }
    }
}
