using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SweetShop.Data;
using SweetShop.Model;
using Microsoft.AspNetCore.Http.Extensions;
using System.Text.Json;
using Microsoft.Extensions.Logging;


namespace SweetStall.Pages
{
    public class AddToCartModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<ShoppingCart> ShoppingCarts { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public static List<CartItemViewModel>? CartItems { get; set; }

        public AddToCartModel(ApplicationDbContext context)
        {
            _context = context;
            CartItems = new List<CartItemViewModel>();
            ShoppingCarts = new List<ShoppingCart>();
        }
        public void OnGet()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (!string.IsNullOrEmpty(userEmail))
            {
                ShoppingCartItems = _context.ShoppingCartItems
                    .Where(item => item.ShoppingCart!.UserEmail == userEmail)
                    .ToList();
            }
            CartItems = HttpContext.Session.GetString("CartItems") != null
                    ? JsonSerializer.Deserialize<List<CartItemViewModel>>(HttpContext.Session.GetString("CartItems")!)!
                    : new List<CartItemViewModel>();
        }

        public IActionResult OnPostOrderConfirmation()
        {
            var isLogged = HttpContext.Session.GetString("IsLogged");
            if (isLogged != "true")
            {
                return RedirectToPage("/SignIn");
            }
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (!string.IsNullOrEmpty(userEmail))
            {
                ShoppingCartItems = _context.ShoppingCartItems
                    .Where(item => item.ShoppingCart!.UserEmail == userEmail)
                    .ToList();
            }
            if (ShoppingCartItems.Count > 0)
            {
                foreach (var cartItem in ShoppingCartItems)
                {
                    var order = new Order
                    {
                        OrderDate = DateTime.Now.Date,
                        Name = cartItem.ItemName,
                        Quantity = cartItem.Quantity,
                        Price = cartItem.FinalPrice
                    };
                    //_context.Orders.Add(order);
                    _context.SaveChanges();
                }
                return RedirectToPage("/PlaceOrder");
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }

        public IActionResult OnPostAddToCart(string name, int quantity, float finalPrice)
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");

            if (!string.IsNullOrEmpty(userEmail))
            {
                ShoppingCart shoppingCart = _context.ShoppingCarts
                    .Include(cart => cart.Items)
                    .FirstOrDefault(cart => cart.UserEmail == userEmail)!;

                if (shoppingCart == null)
                {
                    shoppingCart = new ShoppingCart
                    {
                        UserEmail = userEmail,
                        Items = new List<ShoppingCartItem>()
                    };
                    shoppingCart.Items?.Add(new ShoppingCartItem
                    {
                        ItemName = name,
                        Quantity = quantity,
                        FinalPrice = finalPrice
                    });
                    _context.ShoppingCarts.Add(shoppingCart);
                    _context.ShoppingCartItems.AddRange(shoppingCart.Items!);
                    _context.SaveChanges();
                }
                else
                {
                    _context.ShoppingCartItems.Add(new ShoppingCartItem
                    {
                        ItemName = name,
                        Quantity = quantity,
                        FinalPrice = finalPrice,
                        ShoppingCart = shoppingCart
                    });
                    _context.SaveChanges();
                }
                ShoppingCartItems = _context.ShoppingCartItems
                    .Where(item => item.ShoppingCart!.UserEmail == userEmail)
                    .ToList();
                return Page();
            }
            else
            {
                CartItems = HttpContext.Session.GetString("CartItems") != null
                    ? JsonSerializer.Deserialize<List<CartItemViewModel>>(HttpContext.Session.GetString("CartItems")!)!
                    : new List<CartItemViewModel>();

                CartItems?.Add(new CartItemViewModel
                {
                    Name = name,
                    Quantity = quantity,
                    FinalPrice = finalPrice
                });
                HttpContext.Session.SetString("CartItems", JsonSerializer.Serialize(CartItems));
                return Page();
            }
        }

        public IActionResult OnPostRemoveItem(int itemId)
        {
            var itemToRemove = _context.ShoppingCartItems.Find(itemId);
            if (itemToRemove != null)
            {
                _context.ShoppingCartItems.Remove(itemToRemove);
                _context.SaveChanges();
            }
            return RedirectToPage("/AddToCart");
        }

        public IActionResult OnPostRemoveItemFromList(int ItemIndex)
        {
            CartItems = HttpContext.Session.GetString("CartItems") != null
                    ? JsonSerializer.Deserialize<List<CartItemViewModel>>(HttpContext.Session.GetString("CartItems")!)!
                    : new List<CartItemViewModel>();
            CartItems?.RemoveAt(ItemIndex);
            HttpContext.Session.SetString("CartItems", JsonSerializer.Serialize(CartItems));
            return RedirectToPage("/AddToCart");
        }
    }
}
