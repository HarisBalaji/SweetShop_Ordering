using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SweetShop.Data;
using System.ComponentModel.DataAnnotations;

namespace SweetShop.Pages
{
    public class SignInModel : PageModel
    {
        [BindProperty]
        public UserLogin userl { get; set; } = new UserLogin();

        private readonly ApplicationDbContext _context;

        public SignInModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostValidate()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var admin = _context.Admin.FirstOrDefault(a => (a.Email == userl.EmailOrUsername || a.Name == userl.EmailOrUsername) && a.Password == userl.Password);
            if (admin != null)
            {
                HttpContext.Session.SetString("IsAdmin", "true");
                HttpContext.Session.SetString("IsUser", "false");
                HttpContext.Session.SetString("IsLogged", "true");
                if (!string.IsNullOrEmpty(userl.EmailOrUsername))
                {
                    HttpContext.Session.SetString("UserEmail", userl.EmailOrUsername);
                }
                return RedirectToPage("/Index");
            }

            bool isValidCredentials = ValidateCredentials(userl.EmailOrUsername, userl.Password);

            if (isValidCredentials)
            {
                HttpContext.Session.SetString("IsAdmin", "false");
                HttpContext.Session.SetString("IsUser", "true");
                HttpContext.Session.SetString("IsLogged", "true");
                if (!string.IsNullOrEmpty(userl.EmailOrUsername))
                {
                    HttpContext.Session.SetString("UserEmail", userl.EmailOrUsername);
                }
                return RedirectToPage("/Index");
            }
            else
            {
                ModelState.AddModelError("userl.Password", "Invalid username/email or password");
                return Page();
            }
        }

        private bool ValidateCredentials(string? emailOrUsername, string? password)
        {
            // Check if a user with the provided email or username and password exists in the database
            var user = _context.User.FirstOrDefault(u => (u.Email == emailOrUsername || u.Name == emailOrUsername) && u.Password == password);
            return user != null;
        }

    }

    public class UserLogin
    {
        [Required(ErrorMessage = "Email or username is required")]
        public string? EmailOrUsername { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
