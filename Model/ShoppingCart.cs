using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SweetShop.Model
{
    public class ShoppingCart
    {
        public int CartId { get; set; }
        public string? UserEmail { get; set; }
        public List<ShoppingCartItem>? Items { get; set; }
    }

}
