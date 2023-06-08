using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementTasks.Models
{
    public class Todo
    {

        public int Id { get; set; } = 0;
        public string Title { get; set; } = "task title";
        public string description { get; set; } = "task description";
        public DateTime DueDate { get; set; } = new DateTime(1998, 11, 16);

        public User? user { get; set; }

        public Todo()
        {

        }
    }
}