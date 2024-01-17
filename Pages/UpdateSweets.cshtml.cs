//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.EntityFrameworkCore;
//using SweetShop.Data;
//using SweetShop.Model;
//using System.ComponentModel.DataAnnotations;

//namespace SweetShop.Pages
//{
//    public class UpdateStockModel : PageModel
//    {
//        private readonly ApplicationDbContext _context;

//        [BindProperty]
//        public List<SweetsDB> Sweets { get; set; } = new List<SweetsDB>();

//        [BindProperty]
//        public SweetsDB NewSweet { get; set; }

//        public UpdateStockModel(ApplicationDbContext context)
//        {
//            _context = context;
            
//            NewSweet = new SweetsDB();
//        }


//        public IActionResult OnGet()
//        {
//            if (!IsUserAdmin())
//            {
//                return RedirectToPage("/SignIn");
//            }
//            Sweets = _context.SweetDB.ToList();
//            return Page();
//        }

//        public IActionResult OnPostAddSweet()
//        {
//            if (NewSweet.Name != null && NewSweet.Price > 0)
//            {
//                if (ModelState.IsValid)
//                {
//                    _context.SweetDB.Add(NewSweet);
//                    _context.SaveChanges();
//                    return RedirectToPage("/UpdateSweets");
//                }
//            }
//            return RedirectToPage("/UpdateSweets");
//        }

//        public IActionResult OnPostRemove()
//        {
//            var sweetToRemove = _context.SweetDB.FirstOrDefault(s => s.Id == NewSweet.Id);
//            if (sweetToRemove != null)
//            {
//                _context.SweetDB.Remove(sweetToRemove);
//                _context.SaveChanges();
//            }
//            return RedirectToPage("/UpdateSweets");
//        }

//        public IActionResult OnPostUpdate(int id, int addStock, int currPrice)
//        {
//            // Retrieve the sweet by id from the database
//            var sweetToUpdate = _context.SweetDB.Find(id);

//            if (sweetToUpdate == null)
//            {
//                return RedirectToPage("/Index");
//            }

//            // Update the stock based on the input value
//            sweetToUpdate.Stock += addStock;

//            if (currPrice > 0)
//            {
//                sweetToUpdate.Price = currPrice;
//            }

//            // Save the changes to the database
//            _context.SaveChanges();

//            // Redirect to the same page or a different page as needed
//            return RedirectToPage("/UpdateSweets");
//        }

//        public bool IsUserAdmin()
//        {
//            bool isAdmin = HttpContext?.Session.GetString("IsAdmin") == "true";
//            return isAdmin;
//        }
//    }
//}