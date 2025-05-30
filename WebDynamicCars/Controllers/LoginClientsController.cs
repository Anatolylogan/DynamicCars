using Domain.Entities;
using Domain.UseCase;
using Microsoft.AspNetCore.Mvc;
using WebDynamicCars.Session;

namespace WebDynamicCars.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginClientsController : ControllerBase
    {
        private readonly LoginClientUseCase _loginClientUseCase;
        private readonly ClientSessionService _clientSession;

        public LoginClientsController(LoginClientUseCase loginClientUseCase, ClientSessionService clientSession)
        {
            _loginClientUseCase = loginClientUseCase;
            _clientSession = clientSession;
        }

        [HttpGet("login/{clientId}")]
        public IActionResult Login(int clientId)
        {
            try
            {
                var client = _loginClientUseCase.Execute(clientId);
                _clientSession.Login(clientId);
                return Ok(new { message = "Вы успешно авторизировались!", client });
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}
