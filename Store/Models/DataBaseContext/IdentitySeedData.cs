using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
namespace Store.Models.DataBaseContext
{
    public class IdentitySeedData
    {
        #region private fields

        private const string _adminUser = "Admin";
        private const string _adminPassword = "Secret123$";

        #endregion

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            UserManager<IdentityUser> userManager = app.ApplicationServices
                    .GetRequiredService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(_adminUser);

            if (user == null)
            {
                user = new IdentityUser("Admin");
                await userManager.CreateAsync(user, _adminPassword);
            }
        }
    }
}