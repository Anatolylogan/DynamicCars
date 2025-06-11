using Application.UseCase;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models.Requests;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("api/delivery")]
    public class DeliveryController : ControllerBase
    {
        private readonly SendToStoreUseCase _sendToStoreUseCase;
        private readonly SendToDeliveryUseCase _sendToDeliveryUseCase;

        public DeliveryController(SendToStoreUseCase sendToStoreUseCase,
            SendToDeliveryUseCase sendToDeliveryUseCase)
        {
            _sendToStoreUseCase = sendToStoreUseCase;
            _sendToDeliveryUseCase = sendToDeliveryUseCase;
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

        [HttpPost("send/to/delivery")]
        public IActionResult SendToDelivery([FromBody] SendToDeliveryRequest request)
        {
            if (request.OrderId <= 0 || string.IsNullOrWhiteSpace(request.Address))
                return BadRequest("Неверные данные запроса.");

            try
            {
                _sendToDeliveryUseCase.Execute(request.OrderId, request.Address);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
