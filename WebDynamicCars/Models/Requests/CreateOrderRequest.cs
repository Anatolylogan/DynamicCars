namespace DynamicCarsNew.Models.Requests
{
    public class CreateOrderRequest
    {
        public int ClientId { get; set; }
        public string DeliveryType { get; set; }
        public List<MatChoiceDto> MatChoices { get; set; }
    }

    public class MatChoiceDto
    {
        public string Color { get; set; }
        public string CarBrand { get; set; }
    }
}
