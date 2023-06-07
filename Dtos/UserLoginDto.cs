using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementTasks.Dtos
{
    public class UserLoginDto
    {
        public string Name { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}