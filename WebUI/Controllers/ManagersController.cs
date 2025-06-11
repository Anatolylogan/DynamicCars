using Application.UseCase;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/managers")]
public class ManagersController : ControllerBase
{
    private readonly RegisterManagerUseCase _registerManagerUseCase;
    private readonly LoginManagerUseCase _loginManagerUseCase;
    public ManagersController(
     RegisterManagerUseCase registerManagerUseCase,
     LoginManagerUseCase loginManagerUseCase)
    {
        _registerManagerUseCase = registerManagerUseCase;
        _loginManagerUseCase = loginManagerUseCase;
    }

    [HttpPost("register")]
    public IActionResult RegisterManager([FromBody] RegisterManagerRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.ManagerName))
            return BadRequest("Имя менеджера не может быть пустым.");

        try
        {
            var manager = _registerManagerUseCase.Execute(request.ManagerName);
            return Ok(manager);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginManagerRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.ManagerName))
            return BadRequest("Имя менеджера не может быть пустым.");

        try
        {
            var manager = _loginManagerUseCase.Execute(request.ManagerName);
            return Ok(manager);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}





