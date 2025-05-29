using DynamicCarsNew.Infrastructure;
using Domain.UseCase;
using DynamicCarsNew.Models.Requests;
using Microsoft.AspNetCore.Mvc;


namespace DynamicCarsNew.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly CreateOrderUseCase _createOrderUseCase;

        public OrdersController(CreateOrderUseCase createOrderUseCase)
        {
            _createOrderUseCase = createOrderUseCase;
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] CreateOrderRequest request)
        {
            IDeliveryOption deliveryOption = request.DeliveryType.ToLower() switch
            {
                "pickup" => new PickupOption(),
                _ => throw new ArgumentException("Неверный тип доставки")
            };

            var matChoices = request.MatChoices
                .Select(mc => (mc.Color, mc.CarBrand))
                .ToList();

            try
            {
                _createOrderUseCase.Execute(request.ClientId, matChoices, deliveryOption);
                return Ok("Заказ создан успешно.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}