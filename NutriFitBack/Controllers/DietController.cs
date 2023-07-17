using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NutriFit.Application.Input.Commands.DietContext;
using NutriFit.Application.Input.Commands.DietContext.DTO;
using NutriFit.Application.Input.Receivers.Diet;
using NutriFit.Application.Output.DTOs;
using NutriFit.Application.Output.Interfaces;
using System.Security.Claims;
using System.Text.Json;

namespace NutriFitBack.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class DietController : ControllerBase
    {
        private const string dietGptURL = "https://uvuykll6f4nomzsmm766jwssdm0hsgpb.lambda-url.us-east-2.on.aws/";
        private readonly IReadDietRepository _repository;
        private readonly InsertMealReceiver _insertMealReceiver;
        private readonly InsertDietReceiver _insertDietReceiver;
        private readonly UpdateDietStatusReceiver _updateDietStatusReceiver;

        private static readonly HttpClient _client = new();


        public DietController(IReadDietRepository repository, InsertMealReceiver insertMealReceiver, InsertDietReceiver insertDietReceiver, UpdateDietStatusReceiver updateDietStatusReceiver)
        {
            _repository = repository;
            _insertMealReceiver = insertMealReceiver;
            _insertDietReceiver = insertDietReceiver;
            _updateDietStatusReceiver = updateDietStatusReceiver;
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

        [HttpPatch]
        [Route("")]
        [Authorize]
        public ActionResult<IEnumerable<DietsDTO>> UpdateCurrentDiet([FromBody] int dietId)
        {
            try
            {
                var userId = getUserIdFromToken();
                UpdateDietStatusCommand updateDietStatusCommand = new(userId, dietId);
                var result = _updateDietStatusReceiver.Action(updateDietStatusCommand);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }

        }

        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<DietsDTO>>> AddDietAsync([FromBody] InsertDietDTO insertDietDTO)
        {
            try
            {
                var userId = getUserIdFromToken();
                var schedules = new List<int> ();
                var responses = new List<DishScheduleDietContext>();

                //GTP 3.5 aceita apenas 3 requests por minuto. Gambiarra provisória.
                var taskList = new[]
                {
                  RequestDietGPT(DayOfWeek.Monday, insertDietDTO),
                  RequestDietGPT(DayOfWeek.Tuesday, insertDietDTO),
                  RequestDietGPT(DayOfWeek.Wednesday, insertDietDTO)
                };

                var completedTasks = await Task.WhenAll(taskList);

                for (int i = 0; i < taskList.Length; i++)
                {
                    responses.Add(completedTasks[i]);
                };

                Thread.Sleep(60000);

                taskList = new[]
                {
                  RequestDietGPT(DayOfWeek.Thursday, insertDietDTO),
                  RequestDietGPT(DayOfWeek.Friday, insertDietDTO),
                  RequestDietGPT(DayOfWeek.Saturday, insertDietDTO)
                };

                completedTasks = await Task.WhenAll(taskList);

                for (int i = 0; i < taskList.Length; i++)
                {
                    responses.Add(completedTasks[i]);
                };

                Thread.Sleep(60000);

                taskList = new[]
                {
                  RequestDietGPT(DayOfWeek.Sunday, insertDietDTO)
                };

                completedTasks = await Task.WhenAll(taskList);

                for (int i = 0; i < taskList.Length; i++)
                {
                    responses.Add(completedTasks[i]);
                };

                foreach (var r in responses)
                {
                    var mealCommand = new InsertMealCommand(r);
                    var scheduleState = _insertMealReceiver.Action(mealCommand);

                    if (scheduleState.StatusCode != 200)
                        throw new HttpRequestException(scheduleState.Message, new Exception(""), (System.Net.HttpStatusCode)scheduleState.StatusCode);
                
                    schedules.Add(Convert.ToInt32(scheduleState.Data));

                }
                var dietCommand = new InsertDietCommand(userId, insertDietDTO.DietName, insertDietDTO.DietGoal, schedules[0], schedules[1], schedules[2], schedules[3],schedules[4], schedules[5], schedules[6]);
                var dietState = _insertDietReceiver.Action(dietCommand);

                return Ok(dietState);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode((int)ex.StatusCode.Value, ex.Message);
                 
            }
            catch (Exception ex)
            {
                return StatusCode(500,(ex.Message));
            }

        }

        [HttpGet]
        [Route("{dietId}")]
        [Authorize]
        public ActionResult<IEnumerable<DietsDTO>> GetDiets(int dietId)
        {
            try
            {
                var userId = getUserIdFromToken();
                var diet = _repository.GetDiet(dietId);
                if (diet.UserId != userId) throw new Exception("você não tem permissão para visualizar essa página");

                return Ok(diet);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }

        }

        [HttpGet]
        [Route("DietSchedule/{dietScheduleId}")]
        [Authorize]
        public ActionResult<DishScheduleDTO> GetDietSchedule(int dietScheduleId)
        {
            try
            {
                var userId = getUserIdFromToken();
                var diet = _repository.GetDietSchedule(dietScheduleId);

                return Ok(diet);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }

        }
        [HttpGet]
        [Route("todayDiet")]
        [Authorize]
        public ActionResult<DishScheduleDTO> GetTodayDietByUserId()
        {
            try
            {
                var userId = getUserIdFromToken();
                var diets = _repository.GetDiets(userId).ToArray();
                DishScheduleDTO diet = new DishScheduleDTO();
                DietsDTO currentDiet = new DietsDTO();
                foreach (var d in diets)
                {
                    if (d.CurrentActive)
                    {
                        currentDiet = d;
                    }
                }
                diet = _repository.GetTodayDiet(currentDiet);

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
        private static async Task<DishScheduleDietContext> RequestDietGPT(DayOfWeek dayOfWeekEnum, InsertDietDTO insertDietDTO)
        {
            var jsonBody = JsonSerializer.Serialize(insertDietDTO);
            var jObject = JObject.Parse(jsonBody);
            try
            {
                var dayOfWeek = (dayOfWeekEnum).ToString();
                if ((int)dayOfWeekEnum > 0)
                {
                    jObject.Remove("DayOfWeek");
                }
                jObject.Add("DayOfWeek", dayOfWeek);

                var resultAsJsonString = jObject.ToString();

                var response = await _client.PostAsJsonAsync(dietGptURL, resultAsJsonString);

                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = response.Content.ReadAsStringAsync().Result;
                    if (dataObjects.Length > 0)
                    {
                        var resp = JsonSerializer.Deserialize<DishScheduleDietContext>(dataObjects!);
                        if (resp != null)
                        {
                            return resp;
                        }
                    }
                }
                throw new HttpRequestException("Falha ao requisitar dietas", new Exception(""), response.StatusCode);
            }
            catch (Exception ex)
            {
                throw;
            }

        } 
    }

}