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
    private readonly AssignMakerToOrderUseCase _assignMakerToOrderUseCase;
    private readonly CompleteMakingUseCase _completeMakingUseCase;
    private readonly SendToStoreUseCase _sendToStoreUseCase;

    public ManagersController(
     RegisterManagerUseCase registerManagerUseCase,
     LoginManagerUseCase loginManagerUseCase,
     IOrderRepository orderRepository,
     IClientRepository clientRepository,
     AssignMakerToOrderUseCase assignMakerToOrderUseCase,
     CompleteMakingUseCase completeMakingUseCase,
     SendToStoreUseCase sendToStoreUseCase)
    {
        _registerManagerUseCase = registerManagerUseCase;
        _loginManagerUseCase = loginManagerUseCase;
        _orderRepository = orderRepository;
        _clientRepository = clientRepository;
        _assignMakerToOrderUseCase = assignMakerToOrderUseCase;
        _completeMakingUseCase = completeMakingUseCase;
        _sendToStoreUseCase = sendToStoreUseCase;
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

    [HttpPost("assign/maker")]
    public IActionResult AssignMaker([FromBody] AssignMakerRequest request)
    {
        if (request.OrderId <= 0 || string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Некорректные данные для назначения изготовителя.");

        try
        {
            _assignMakerToOrderUseCase.Execute(request.OrderId, request.Name);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    [HttpPost("complete/order")]
    public IActionResult CompleteOrder([FromBody] CompleteOrderRequest request)
    {
        if (request.OrderId <= 0 || string.IsNullOrWhiteSpace(request.ClientEmail))
            return BadRequest("Некорректные данные для завершения заказа.");

        try
        {
            _completeMakingUseCase.Execute(request.OrderId, request.ClientEmail);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    [HttpPost("send/to/store")]
    public IActionResult SendToStore([FromBody] SendToStoreRequest request)
    {
        if (request.OrderId <= 0)
            return BadRequest("Некорректный идентификатор заказа.");

        try
        {
            _sendToStoreUseCase.Execute(request.OrderId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}




