using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SweetShop.Data;
using SweetShop.Model;

namespace SweetStall.Pages
{
    public class DisplayItemsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public List<Category> Categories { get; set; } = new List<Category>();
        [BindProperty]
        public List<CategoryItem> CategoryItems { get; set; } = new List<CategoryItem>();

        public DisplayItemsModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Categories = _context.Categories.ToList();
            Categories = _context.Categories.Include(c => c.Items).ToList();
        }
    }
}
