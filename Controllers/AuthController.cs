using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ManagementTasks.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;

        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }


        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto userRequest)
        {
            var response = await _repo.Register(
                new User { Name = userRequest.Name },
                userRequest.password
            );

            if (!response.success)
            {
                return BadRequest(response);
            }

            return Ok(response);


        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(UserLoginDto userRequest)
        {
            var response = await _repo.Login(
                userRequest.Name,
                userRequest.password
            );

            if (!response.success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

    }
}