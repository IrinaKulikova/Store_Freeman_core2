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
            StoreContext context = app.ApplicationServices
                                      .GetRequiredService<StoreContext>();

            context.Database.Migrate();

            if (!context.Categories.Any() && !context.Toys.Any())
            {

                var girls = new Category() {  Name = "Girls" };
                var boys = new Category() {  Name = "Boys" };
                var children = new Category() {  Name = "Children" };


                context.Toys.AddRange(
                new Toy
                {
                    Name = "Doll",
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
                    Name = "Car",
                    Price = 255,
                    Category = boys,
                    Description = "Trunk"
                },
                new Toy
                {
                    Name = "Lol",
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
                });

                context.SaveChanges();
            }
        }
    }
}
