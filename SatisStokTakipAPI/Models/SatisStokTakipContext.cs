using Microsoft.EntityFrameworkCore;

namespace SatisStokTakipAPI.Models
{
    public class SatisStokTakipContext : DbContext
    {
        public SatisStokTakipContext(DbContextOptions<SatisStokTakipContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
