using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementTasks.Dtos
{
    public class addTodoDto
    {
        public string Title { get; set; } = "task title";
        public string description { get; set; } = "task description";
        public DateTime DueDate { get; set; } = new DateTime(1998, 11, 16);
    }
}