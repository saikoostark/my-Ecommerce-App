using Microsoft.EntityFrameworkCore;

namespace my_Ecommerce_App.Models;

public class EcommerceDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categorys { get; set; }
    public DbSet<ProductCategory> ProductCategorys { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=SAIKOO\SAIKOO;Database=Ecommerce;Trusted_connection=True;Encrypt=False;");
    }
}