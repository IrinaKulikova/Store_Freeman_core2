﻿using Microsoft.EntityFrameworkCore;

namespace Store.Models.DataBaseContext
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
                            : base(options) { }

        public DbSet<Toy> Toys { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartLine> CartLines { get; set; }
    }
}
