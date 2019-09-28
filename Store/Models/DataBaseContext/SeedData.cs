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

            if (!context.Toys.Any())
            {
                context.Toys.AddRange(
                new Toy
                {
                    Name = "Doll",
                    Price = 250,
                    Category = "Girls",
                    Description = "Bella"
                },
                new Toy
                {
                    Name = "Ball",
                    Price = 179,
                    Category = "Children",
                    Description = "Football"
                },
                new Toy
                {
                    Name = "Car",
                    Price = 255,
                    Category = "Boys",
                    Description = "Trunk"
                },
                new Toy
                {
                    Name = "Lol",
                    Price = 550,
                    Category = "Girls",
                    Description = "RockStar"
                },
                new Toy
                {
                    Name = "Ball",
                    Price = 209,
                    Category = "Children",
                    Description = "Socket"
                },
                new Toy
                {
                    Name = "Car",
                    Price = 555,
                    Category = "Boys",
                    Description = "Bilaz"
                }
                );

                context.SaveChanges();
            }
        }
    }
}
