using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriFit.Application.Input.Commands.UserContext;
using NutriFit.Application.Input.Receivers.User;
using NutriFit.Application.Output.DTOs;
using NutriFit.Application.Output.Interfaces;
using System.Security.Claims;

namespace NutriFitBack.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class DietController : ControllerBase
    {

        private readonly IReadDietRepository _repository;

        public DietController(IReadDietRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("")]
        [Authorize]
        public ActionResult<IEnumerable<DietsDTO>> GetDiets()
        {
            try
            {
                var userId = getUserIdFromToken();
                var diets = _repository.GetDiets(userId);

                return Ok(diets);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }

        }

        [HttpGet]
        [Route("{dietScheduleId}")]
        [Authorize]
        public ActionResult<DishScheduleDTO> GetDiet(int dietScheduleId)
        {
            try
            {
                var userId = getUserIdFromToken();
                var diet = _repository.GetDiet(dietScheduleId);

                return Ok(diet);
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