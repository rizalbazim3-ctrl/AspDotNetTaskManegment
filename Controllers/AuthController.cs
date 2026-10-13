using Microsoft.AspNetCore.Mvc;
using TaskList.Services;
using TaskList.Models;
using TaskList.DTOs;

namespace TaskList.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public IActionResult addUser(RegistrationDto data)
        {
            bool add = _authService.Register(data);
            Console.WriteLine("auth");

            if(!add)
            {
                return Conflict("userName already exist");
            }
            
            return StatusCode(201,"something is created");
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginDto data)
        {
           string? token =  _authService.Login(data);
            if ( token == null)
            {
                return Unauthorized("the user not exist");
            }

            //Why use Ok(new { token = token })?
            //It returns a JSON object containing your JWT.
            return Ok(new
                {
                    token = token
                });
        }
    }
}
