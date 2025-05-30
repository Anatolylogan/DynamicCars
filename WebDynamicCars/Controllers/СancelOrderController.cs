using Microsoft.AspNetCore.Mvc;
using Domain.UseCase;
using WebDynamicCars.Session;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly CancelOrderUseCase _cancelOrderUseCase;
    private readonly ClientSessionService _clientSession; 

    public OrderController(CancelOrderUseCase cancelOrderUseCase, ClientSessionService clientSession)
    {
        _cancelOrderUseCase = cancelOrderUseCase;
        _clientSession = clientSession;
    }

    [HttpPost("{orderId}/cancel")]
    public IActionResult CancelOrder(int orderId)
    {
        if (!_clientSession.IsLoggedIn())
        {
            return Unauthorized(new { error = "Вы должны войти как клиент, чтобы отменить заказ." });
        }

        var clientId = _clientSession.GetCurrentClientId();

        if (clientId == null)
        {
            return Unauthorized(new { error = "Не удалось определить клиента." });
        }

        try
        {
            _cancelOrderUseCase.Execute(orderId, clientId.Value);
            return Ok(new { message = $"Заказ с ID {orderId} успешно отменен." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
