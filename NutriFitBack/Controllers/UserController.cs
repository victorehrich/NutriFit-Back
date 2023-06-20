using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriFit.Application.Input.Commands.UserContext;
using NutriFit.Application.Input.Receivers.User;
using NutriFit.Application.Output.Interfaces;
using System.Security.Claims;

namespace NutriFitBack.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly InsertUserReceiver _insertUserReceiver;
        private readonly UpdateUserReceiver _updateUserReceiver;

        private readonly IReadUserRepository _repository;

        public UserController(InsertUserReceiver receiver, UpdateUserReceiver updateUserReceiver, IReadUserRepository repository)
        {
            _repository = repository;
            _insertUserReceiver = receiver;
            _updateUserReceiver = updateUserReceiver;
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public ActionResult<dynamic> CreateUser([FromBody] UserCommand command)
        {
            var state = _insertUserReceiver.Action(command);
            return state;
        }
        [HttpGet]
        [Route("")]
        [Authorize]
        public ActionResult<dynamic> GetUser()
        {
            try
            {
                var userId = getUserIdFromToken();
                var user = _repository.GetUsersById(userId);
                if (user is null) return NotFound("usu�rio n�o encontrado");
                user.Password = "";

                return user;
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }

        }
        [HttpPut]
        [Route("")]
        [Authorize]
        public ActionResult<dynamic> UpdateUser([FromBody] UpdateUserCommand command)
        {
            try
            {
                var userId = getUserIdFromToken();
                if (userId != command.UserId) throw new Exception("Unauthorized");
                var state = _updateUserReceiver.Action(command);
                return state;
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }

        }
        private int getUserIdFromToken()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity == null)
                {
                    throw new Exception("Unauthorized");
                }

                IEnumerable<Claim> claims = identity.Claims;
                var userId = int.Parse(identity.FindFirst("Id").Value);
                return userId;
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong!");
            }
        }
    }
}