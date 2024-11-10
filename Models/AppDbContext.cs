using Microsoft.EntityFrameworkCore;


namespace WebApplication1TEST.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Shipping> Shipping { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }
        


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
            
            Database.EnsureCreated();// роверка что поля работают конкретно
        } 
    }
}
