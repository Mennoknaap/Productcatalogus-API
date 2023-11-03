using Microsoft.EntityFrameworkCore;

namespace ProductCatalogus
{
    public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
        public DbSet<Product> Products { get; set; }
    }
}