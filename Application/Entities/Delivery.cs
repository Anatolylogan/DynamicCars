namespace Application.Entities
{
    public class Delivery : Entity
    {
        public int OrderId {  get; set; }
        public string Address { get; set; }
    }
}