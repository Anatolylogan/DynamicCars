using System.Reflection;
using Domain.UseCase;
using Domain.Сontracts;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models.Requests;

[ApiController]
[Route("api/[controller]")]
public class ManagersController : ControllerBase
{
    private readonly RegisterManagerUseCase _registerManagerUseCase;
    private readonly LoginManagerUseCase _loginManagerUseCase;
    private readonly IOrderRepository _orderRepository;
    private readonly IClientRepository _clientRepository;


    public ManagersController(
     RegisterManagerUseCase registerManagerUseCase,
     LoginManagerUseCase loginManagerUseCase,
     IOrderRepository orderRepository,
     IClientRepository clientRepository)
    {
        _registerManagerUseCase = registerManagerUseCase;
        _loginManagerUseCase = loginManagerUseCase;
        _orderRepository = orderRepository;
        _clientRepository = clientRepository;
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
    [HttpGet("orders")]
    public IActionResult GetOrders()
    {
        var orders = _orderRepository.GetAll();

        if (!orders.Any())
            return NotFound("Список заказов пуст.");

        var result = orders.Select(order =>
        {
            var client = _clientRepository.GetById(order.ClientId);
            return new OrderResponse
            {
                OrderId = order.Id,
                ClientId = order.ClientId,
                ClientName = client?.Name ?? "Неизвестный клиент",
                ClientPhone = client?.PhoneNumber ?? "Неизвестный номер телефона",
                CarBrand = order.Items.FirstOrDefault()?.CarBrand ?? "Не задан",
                CarpetColor = order.Items.FirstOrDefault()?.CarpetColor ?? "Не задан",
                DeliveryDetails = order.DeliveryDetails,
                DeliveryCost = order.DeliveryCost,
                Status = order.Status.ToString()
            };
        }).ToList();

        return Ok(result);
    }
}
