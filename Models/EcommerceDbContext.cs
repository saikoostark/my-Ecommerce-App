using Microsoft.EntityFrameworkCore;

namespace my_Ecommerce_App.Models
{
    public class EcommerceDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=SAIKOO\SAIKOO;Database=Ecommerce;Trusted_connection=True;Encrypt=False;");
        }
    }
}