using Microsoft.EntityFrameworkCore;

namespace Store.Models.DataBaseContext
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options) { }

        public DbSet<Toy> Toys { get; set; }
    }
}
