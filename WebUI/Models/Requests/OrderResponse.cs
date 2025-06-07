namespace WebUI.Models.Requests
{
    public class OrderResponse
    {
        public int OrderId { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public string CarBrand { get; set; }
        public string CarpetColor { get; set; }
        public string DeliveryDetails { get; set; }
        public decimal DeliveryCost { get; set; }
        public string Status { get; set; }
    }

}
