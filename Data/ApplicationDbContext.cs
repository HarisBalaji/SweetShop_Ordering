using Microsoft.EntityFrameworkCore;
using SweetShop.Model;

namespace SweetShop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserRegister> User { get; set; }
        public DbSet<AdminRegister> Admin { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryItem> CategoryItems { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRegister>().HasKey(u => u.Email);
            modelBuilder.Entity<AdminRegister>().HasKey(u => u.Email);

            modelBuilder.Entity<ShoppingCart>()
                .HasKey(sc => sc.CartId);

            modelBuilder.Entity<ShoppingCart>()
                .Property(sc => sc.UserEmail)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnType("VARCHAR(30)");

            modelBuilder.Entity<ShoppingCartItem>()
                .HasKey(item => item.ItemId);

            modelBuilder.Entity<ShoppingCartItem>()
                .Property(item => item.ItemName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnType("VARCHAR(30)");
        }
    }
}
