namespace SweetShop.Model
{
    public class CategoryItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public float Price { get; set; }
        public string? ImageUrl { get; set; }

        // Foreign key to relate items to a category
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}
