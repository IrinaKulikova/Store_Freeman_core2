using System.Collections.Generic;
using System.Linq;
using Store.Models;
using Store.Repositories.Interfaces;

namespace Store.Repositories
{
    public class FakeToyRepository : IToyRepository
    {
        public IQueryable<Toy> Toys => new List<Toy>() {
            new Toy { Name = "Doll", Price = 250 },
            new Toy { Name = "Ball", Price = 179 },
            new Toy { Name = "Car", Price = 255 }
        }.AsQueryable<Toy>();
    }
}
