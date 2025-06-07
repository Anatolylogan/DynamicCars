namespace WebUI.Models.Requests
{
    public class CompleteOrderRequest
    {
        public int OrderId { get; set; }
        public string ClientEmail { get; set; }
    }
}
