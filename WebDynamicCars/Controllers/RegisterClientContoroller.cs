using Domain.Entities;
using Domain.UseCase;
using Microsoft.AspNetCore.Mvc;
using WebDynamicCars.Models.Requests;

namespace WebDynamicCars.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly RegisterClientUseCase _registerClientUseCase;

        public ClientsController(RegisterClientUseCase registerClientUseCase)
        {
            _registerClientUseCase = registerClientUseCase;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterClientRequest request)
        {
            try
            {
                var newClient = _registerClientUseCase.Execute(request.Name, request.PhoneNumber);

                return Ok(new
                {
                    message = "Клиент успешно зарегистрирован!",
                    client = newClient
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}

