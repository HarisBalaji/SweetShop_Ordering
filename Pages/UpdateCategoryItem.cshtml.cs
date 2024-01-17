using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SweetShop.Data;
using SweetShop.Model;

namespace SweetStall.Pages
{
    public class UpdateCategoryItemModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public List<Category> Categories { get; set; } = new List<Category>();
        [BindProperty]
        public List<CategoryItem> CategoryItems { get; set; } = new List<CategoryItem>();

        public UpdateCategoryItemModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Categories = _context.Categories.ToList();
            Categories = _context.Categories.Include(c => c.Items).ToList();
        }

        public IActionResult OnPostAddCategoryItem(int SelectedCategoryId, string itemName, float itemPrice, string itemImageUrl)
        {
            var newItem = new CategoryItem
            {
                Name = itemName,
                Price = itemPrice,
                ImageUrl = itemImageUrl,
                CategoryId = SelectedCategoryId
            };

            _context.CategoryItems.Add(newItem);
            _context.SaveChanges();

            return RedirectToPage("/UpdateCategoryItem");
        }

        public IActionResult OnPostRemoveCategoryItem(int itemId)
        {
            var itemToRemove = _context.CategoryItems.Find(itemId);

            if (itemToRemove != null)
            {
                _context.CategoryItems.Remove(itemToRemove);
                _context.SaveChanges();
            }

            return RedirectToPage("/UpdateCategoryItem");
        }

        public IActionResult OnPostUpdateCategoryItem(int itemId, float itemCurrPrice)
        {
            var itemToUpdate = _context.CategoryItems.Find(itemId);
            if (itemToUpdate != null)
            {
                itemToUpdate.Price = itemCurrPrice;
            }
            _context.SaveChanges();
            return RedirectToPage("/UpdateCategoryItem");
        }
    }
}
