using DynamicCarsNew.Infrastructure;
using Domain.UseCase;
using DynamicCarsNew.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using WebDynamicCars.Session;


namespace DynamicCarsNew.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly CreateOrderUseCase _createOrderUseCase;
        private readonly ClientSessionService _clientSession;
        public OrdersController(CreateOrderUseCase createOrderUseCase, ClientSessionService clientSession)
        {
            _createOrderUseCase = createOrderUseCase;
            _clientSession = clientSession;
        }

        [HttpPost("create")]
        public IActionResult CreateOrder([FromBody] CreateOrderRequest request)
        {
            if (!_clientSession.IsLoggedIn())
            {
                return Unauthorized(new { error = "Вы должны войти как клиент, чтобы создать заказ." });
            }

            var clientId = _clientSession.GetCurrentClientId()!.Value;

            try
            {
                var matChoicesTuples = request.MatChoices
                    .Select(m => (m.Color, m.CarBrand))
                    .ToList();

                _createOrderUseCase.Execute(clientId, matChoicesTuples, new PickupOption());

                return Ok(new { message = "Заказ успешно создан." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
