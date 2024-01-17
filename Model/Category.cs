namespace SweetShop.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // Navigation property to relate items to a category
        public List<CategoryItem>? Items { get; set; }
    }
}
