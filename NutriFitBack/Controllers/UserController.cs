using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriFit.Application.Input.Commands.UserContext;
using NutriFit.Application.Input.Receivers;
using NutriFit.Application.Output.Interfaces;
using NutriFitBack.Services;

namespace NutriFitBack.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly InsertUserReceiver _insertUserReceiver;
        public UserController(InsertUserReceiver receiver)
        {
            _insertUserReceiver = receiver;
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public ActionResult<dynamic> CreateUser([FromBody] UserCommand command)
        {
            var state = _insertUserReceiver.Action(command);
            return state;
        }
    }
}