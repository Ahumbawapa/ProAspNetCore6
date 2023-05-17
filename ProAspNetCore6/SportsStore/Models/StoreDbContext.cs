using Microsoft.EntityFrameworkCore;


namespace SportsStore.Models
{
    // EFCore bietet Zugriff auf eine Datenbank durch eine Context-Klasse
    // 
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            :base(options)
        {
            
        }

        public DbSet<Product> Products => Set<Product>();
    }
}
