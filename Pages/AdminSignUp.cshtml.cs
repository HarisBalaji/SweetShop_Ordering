using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SweetShop.Data;
using SweetShop.Model;
using System.ComponentModel.DataAnnotations;

namespace SweetShop.Pages
{
    public class AdminSignUpModel : PageModel
    {
        [BindProperty]
        public AdminRegister Admin { get; set; } = new AdminRegister();

        private readonly ApplicationDbContext _context;

        public AdminSignUpModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnPostRegister()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (UsernameExists(Admin.Name))
            {
                ModelState.AddModelError("Admin.Name", "Username already exists. Please choose a different one.");
                return Page();
            }

            if (EmailExists(Admin.Email))
            {
                ModelState.AddModelError("Admin.Email", "Already signed up!");
                return Page();
            }
            
            _context.Admin.Add(Admin); // Assuming you have a table for administrators in the database
            _context.SaveChanges();

            return RedirectToPage("/Index");
        }

        private bool UsernameExists(string? username)
        {
            return _context.Admin.Any(u => u.Name == username);
        }

        private bool EmailExists(string? useremail)
        {
            return _context.Admin.Any(u => u.Email == useremail);
        }
    }

    


}
