using Domain.UseCase;
using Microsoft.AspNetCore.Mvc;
using WebDynamicCars.Models.Requests;

namespace WebDynamicCars.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientsController : ControllerBase
    {
        private readonly RegisterClientUseCase _registerClientUseCase;
        private readonly LoginClientUseCase _loginClientUseCase;

        public ClientsController(
            RegisterClientUseCase registerClientUseCase,
            LoginClientUseCase loginClientUseCase)
        {
            _registerClientUseCase = registerClientUseCase;
            _loginClientUseCase = loginClientUseCase;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterClientRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newClient = _registerClientUseCase.Execute(request.Name, request.PhoneNumber);
                return Ok(newClient);
            }
            catch (ArgumentException ex)
            {
                return Conflict(new { error = ex.Message });
            }
        }

        [HttpGet("login/{clientId}")]
        public IActionResult Login(int clientId)
        {
            try
            {
                var client = _loginClientUseCase.Execute(clientId);
                return Ok(client);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}
