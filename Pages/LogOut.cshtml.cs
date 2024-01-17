using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SweetShop.Pages
{
    public class LogOutModel : PageModel
    {
        public IActionResult OnGet()
        {
            HttpContext.Session.SetString("IsAdmin", "false");
            HttpContext.Session.SetString("IsUser", "false");
            HttpContext.Session.SetString("IsLogged", "false");
            HttpContext.Session.Clear();

            return RedirectToPage("/Index");
        }
    }
}
