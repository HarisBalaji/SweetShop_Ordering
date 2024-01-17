using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SweetShop.Model;
using SweetShop.Data;
using System.ComponentModel.DataAnnotations;

namespace SweetShop.Pages
{
    [BindProperties(SupportsGet = true)]
    public class SignUpModel : PageModel
    {
        [BindProperty]
        public UserRegister user { get; set; } = new UserRegister();

        private readonly ApplicationDbContext _context;

        public SignUpModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPostRegister()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (UsernameExists(user.Name))
            {
                ModelState.AddModelError("user.Name", "Username already exists. Please choose a different one.");
                return Page();
            }
            if (emailExists(user.Email))
            {
                ModelState.AddModelError("user.Email", "Already signed up!");
                return Page();
            }
            // Save user to the database
            _context.User.Add(user);
            _context.SaveChanges();
            HttpContext.Session.SetString("IsUser", "true");
            HttpContext.Session.SetString("IsLogged", "true");
            if (!string.IsNullOrEmpty(user.Email))
            {
                HttpContext.Session.SetString("UserEmail", user.Email);
            }
            return RedirectToPage("/Index");
        }
        private bool UsernameExists(string? username)
        {
            return _context.User.Any(u => u.Name == username);
        }

        private bool emailExists(string? useremail)
        {
            return _context.User.Any(u => u.Email == useremail);
        }
    }
}
