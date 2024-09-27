using Microsoft.EntityFrameworkCore;
using rentcar_api.Models;

namespace rentcar_api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Car> Cars { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Manufacturer> Manufacturers { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
    }
}
