using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.ViewModels;
using System.Threading.Tasks;

namespace Store.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        #region private fields

        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;

        #endregion

        #region ctor

        public AccountController(UserManager<IdentityUser> userManager,
                                 SignInManager<IdentityUser> signinManager)
        {
            _userManager = userManager;
            _signInManager = signinManager;
        }

        #endregion

        [AllowAnonymous]
        public ViewResult Login(string returnUrl)
        {
            return View(new LoginModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginModel.Name);

                if (user != null)
                {
                    await _signInManager.SignOutAsync();

                    var result = await _signInManager.PasswordSignInAsync(user,
                                        loginModel.Password, false, false);

                    if (result.Succeeded)
                    {
                        if (loginModel == null || loginModel.ReturnUrl == null)
                        {
                            return RedirectToAction("Index", "Admin");
                        }

                        return Redirect(loginModel?.ReturnUrl);
                    }
                }
            }

            ModelState.AddModelError("", "Invalid name or password");

            return View(loginModel);
        }

        public async Task<RedirectResult> Logout(string returnUrl)
        {
            await _signInManager.SignOutAsync();

            return Redirect(returnUrl ?? "/");
        }
    }
}