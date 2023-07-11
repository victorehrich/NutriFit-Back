using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriFit.Application.Input.Commands.UserContext;
using NutriFit.Application.Input.Commands.UserContext.Bucket;
using NutriFit.Application.Input.Receivers.User;
using NutriFit.Application.Output.DTOs;
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
        private readonly InsertOrUpdateImageReceiver _insertOrUpdateImageReceiver;
        private readonly IConfiguration _config;
        private readonly IReadUserRepository _repository;

        public UserController(InsertUserReceiver receiver, UpdateUserReceiver updateUserReceiver, IReadUserRepository repository, InsertOrUpdateImageReceiver insertOrUpdateImageReceiver, IConfiguration config)
        {
            _repository = repository;
            _insertUserReceiver = receiver;
            _updateUserReceiver = updateUserReceiver;
            _insertOrUpdateImageReceiver = insertOrUpdateImageReceiver;
            _config = config;
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public ActionResult<dynamic> CreateUser([FromBody] UserCommand command)
        {
            var state = _insertUserReceiver.Action(command);
            return state;
        }

        [HttpPost]
        [Route("Image")]
        [Authorize]
        public async Task<ActionResult<dynamic>> UploadUSerImage([FromForm] IFormFile userImage)
        {
            await using var memeryStr = new MemoryStream();
            await userImage.CopyToAsync(memeryStr);
            var fileExt = Path.GetExtension(userImage.FileName);

            var command = new UserImageCommand()
            {
                BucketName = "nutrifit-img",
                Name = userImage.FileName,
                Image = memeryStr,
                AwsKey = _config["AWSConfiguration:AwsBucketAccessKey"],
                AwsSecretKey = _config["AWSConfiguration:AwsBucketSecretKey"]
            };
            var state = await _insertOrUpdateImageReceiver.Action(command);
            return state;
        }

        [HttpGet]
        [Route("Image/{userName}")]
        [Authorize]
        public async Task<ActionResult<dynamic>> GetUsetImage(string userName)
        {
            try
            {
                var userId = getUserIdFromToken();
                var imageUserDTO = new ImageUserDTO()
                {
                    PathFile = userId + " - " + userName,
                    BucketName = "nutrifit-img",
                    AwsKey = _config["AWSConfiguration:AwsBucketAccessKey"],
                    AwsSecretKey = _config["AWSConfiguration:AwsBucketSecretKey"]
                };


                var state = await _repository.DownloadUserImage(imageUserDTO);
                return state;
            }catch (Exception ex)
            {
                return new ReturnGetImageUserDTO();
            }
            
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
                if (user is null) return NotFound("usuário não encontrado");
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
