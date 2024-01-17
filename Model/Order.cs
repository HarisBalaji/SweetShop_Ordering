namespace SweetShop.Model
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
    }

}
