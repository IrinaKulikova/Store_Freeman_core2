using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Models.DataBaseContext;
using Store.Repositories;
using Store.Repositories.Interfaces;

namespace Store
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<StoreContext>(options =>
               options.UseSqlServer(
                   Configuration["ConnectionStrings:StoreConnection"]));
            services.AddTransient<IToyRepository, DBToyRepository>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: null,
                    template: "{category}/Page{toyPage:int}",
                    defaults: new { controller = "Toy", action = "List" });
                routes.MapRoute(
                    name: null,
                    template: "Page{toyPage:int}",
                    defaults: new { controller = "Toy", action = "List", productPage = 1 });

                routes.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new { controller = "Toy", action = "List", productPage = 1 });

                routes.MapRoute(
                    name: null,
                    template: "", defaults: new { controller = "Toy", action = "List", productPage = 1 });
                routes.MapRoute(
                    name: null, 
                    template: "{controller}/{action}/{id?}");
            });

            //SeedData.EnsurePopulated(app);
        }
    }
}
