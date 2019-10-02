using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Store.Models.DataBaseContext
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            var context = app.ApplicationServices
                    .GetRequiredService<StoreContext>();

            context.Database.Migrate();

            if (!context.Categories.Any() && !context.Toys.Any())
            {

                var girls = new Category() { Name = "Girls" };
                var boys = new Category() { Name = "Boys" };
                var children = new Category() { Name = "Children" };

                context.Toys.AddRange(
                new Toy
                {
                    Name = "Doll Barby",
                    Price = 250,
                    Category = girls,
                    Description = "Bella"
                },
                new Toy
                {
                    Name = "Ball",
                    Price = 179,
                    Category = children,
                    Description = "Football"
                },
                new Toy
                {
                    Name = "Large Car",
                    Price = 255,
                    Category = boys,
                    Description = "Trunk"
                },
                new Toy
                {
                    Name = "Lol surprise",
                    Price = 550,
                    Category = girls,
                    Description = "RockStar"
                },
                new Toy
                {
                    Name = "Ball",
                    Price = 209,
                    Category = children,
                    Description = "Socket"
                },
                new Toy
                {
                    Name = "Car",
                    Price = 555,
                    Category = boys,
                    Description = "Bilaz"
                },
                new Toy()
                {
                    Name = "Hot Wheels Track",
                    Price = 1400,
                    Category = boys,
                    Description = "from two years old"
                },
                 new Toy()
                 {
                     Name = "Lego Duplo",
                     Price = 800,
                     Category = children,
                     Description = "from two years old"
                 });

                context.SaveChanges();
            }
        }
    }
}
