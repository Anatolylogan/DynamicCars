using Application.UseCase;
using Application.Сontracts;
using DynamicCarsNew.Infrastructure;
using DynamicCarsNew.Models.Requests;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models.Requests;

namespace WebDynamicCars.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly CreateOrderUseCase _createOrderUseCase;
        private readonly CancelOrderUseCase _cancelOrderUseCase;
        private readonly FilterOrdersByStatusUseCase _filterOrdersByStatusUseCase;
        private readonly CompleteMakingUseCase _completeMakingUseCase;
        private readonly IOrderRepository _orderRepository;
        private readonly IClientRepository _clientRepository;

        public OrderController(
            CreateOrderUseCase createOrderUseCase,
            CancelOrderUseCase cancelOrderUseCase,
            FilterOrdersByStatusUseCase filterOrdersByStatusUseCase,
            CompleteMakingUseCase completeMakingUseCase,
            IOrderRepository orderRepository,
            IClientRepository clientRepository)
        {
            _createOrderUseCase = createOrderUseCase;
            _cancelOrderUseCase = cancelOrderUseCase;
            _filterOrdersByStatusUseCase = filterOrdersByStatusUseCase;
            _completeMakingUseCase = completeMakingUseCase;
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
        }

        [HttpPost("create")]
        public IActionResult CreateOrder([FromBody] CreateOrderRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var matChoices = request.MatChoices
                    .Select(m => (m.Color, m.CarBrand))
                    .ToList();

                var order = _createOrderUseCase.Execute(request.ClientId, matChoices, new PickupOption());
                return Ok(order);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost("{orderId}/cancel")]
        public IActionResult CancelOrder(int orderId, [FromQuery] int clientId)
        {
            if (orderId <= 0 || clientId <= 0)
                return BadRequest(new { error = "Некорректные параметры запроса." });

            try
            {
                _cancelOrderUseCase.Execute(orderId, clientId);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message });
            }
        }
        [HttpPost("orders/by/status")]
        public IActionResult GetOrdersByStatus([FromBody] FilterOrdersRequest request)
        {
            if (!Enum.IsDefined(typeof(OrderStatus), request.Status))
            {
                return BadRequest(new { error = "Недопустимый статус заказа." });
            }

            var orders = _filterOrdersByStatusUseCase.Execute(request.Status);
            return Ok(orders);
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
}

