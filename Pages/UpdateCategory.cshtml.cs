using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SweetShop.Data;
using SweetShop.Model;

namespace SweetStall.Pages
{
    public class UpdateCategoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public List<Category> Categories { get; set; } = new List<Category>();
        [BindProperty]
        public List<CategoryItem> CategoryItems { get; set; } = new List<CategoryItem>();

        public UpdateCategoryModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Categories = _context.Categories.ToList();
        }

        public IActionResult OnPostAddCategory(string categoryName)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var newCategory = new Category
            {
                Name = categoryName
            };

            _context.Categories.Add(newCategory);
            _context.SaveChanges();

            return RedirectToPage("/UpdateCategory");
        }

        public IActionResult OnPostRemoveCategory(int categoryId)
        {
            var categoryToRemove = _context.Categories.Find(categoryId);

            if (categoryToRemove != null)
            {
                _context.Categories.Remove(categoryToRemove);
                _context.SaveChanges();
            }

            return RedirectToPage();
        }
    }
}
