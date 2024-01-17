using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SweetShop.Model
{
    public class ShoppingCartItem
    {
        public int ItemId { get; set; }
        public string? ItemName { get; set; }
        public int Quantity { get; set; }
        public float FinalPrice { get; set; }
        public int ShoppingCartId { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
    }
}
