using Domain.UseCase;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebUI.Models.Requests
{
    public class FilterOrdersRequest
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [Required]
        public OrderStatus Status { get; set; }
    }
}
