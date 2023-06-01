using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriFit.Application.Output.DTOs;
using NutriFit.Application.Output.Interfaces;
using NutriFitBack.Services;

namespace NutriFitBack.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IReadUserRepository repository;
        public AuthController(IReadUserRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult<dynamic> Authenticate([FromBody] LoginDTO login)
        {
            var user = repository.GetUsersByEmail(login.Email);
            var token = TokenService.GenerateToken(user);
            user.Password = "";
            var response = new
            {
                user = user,
                token = token,
            };
            return response;
        }
    }
}