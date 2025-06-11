using Application.UseCase;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models.Requests;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("api/maker")]
    public class MakerController : ControllerBase
    {
        private readonly AssignMakerToOrderUseCase _assignMakerToOrderUseCase;
        
        public MakerController(AssignMakerToOrderUseCase assignMakerToOrderUseCase)
        {
            _assignMakerToOrderUseCase = assignMakerToOrderUseCase;
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
    }
}
