namespace WebUI.Models.Requests
{
    public class SendToDeliveryRequest
    {
        public int OrderId { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}
