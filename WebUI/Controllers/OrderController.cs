using Domain.UseCase;
using DynamicCarsNew.Infrastructure;
using DynamicCarsNew.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using WebDynamicCars.Models.Requests;

namespace WebDynamicCars.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly CreateOrderUseCase _createOrderUseCase;
        private readonly CancelOrderUseCase _cancelOrderUseCase;
    
        public OrderController(
            CreateOrderUseCase createOrderUseCase,
            CancelOrderUseCase cancelOrderUseCase,
            FilterOrdersByStatusUseCase filterOrdersByStatusUseCase)
        {
            _createOrderUseCase = createOrderUseCase;
            _cancelOrderUseCase = cancelOrderUseCase;
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
                var canceledOrder = _cancelOrderUseCase.Execute(orderId, clientId);
                return Ok(canceledOrder);
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
    }
}
