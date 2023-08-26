using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Notebook.Authorization;
using Notebook.Models;

namespace Notebook.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> UserManager, SignInManager<User> SIM)
        {
            this._signInManager = SIM;
            this._userManager = UserManager;
        }
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            if (returnUrl == null) returnUrl = "/Home";
            return View(new UserLogin()
            {
                ReturnUrl = returnUrl 
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLogin model)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _signInManager.PasswordSignInAsync(model.LoginProp,
                    model.Password,
                    false,
                    lockoutOnFailure: false);

                if (loginResult.Succeeded)
                {
                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        if (User.Identity.IsAuthenticated)
                        {
                            return Redirect(model.ReturnUrl);
                        }
                        return Redirect(model.ReturnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }

            }

            ModelState.AddModelError(string.Empty, "User is not found");
            return View(model);
         }
        [HttpGet]
        public IActionResult Registration()
        {
            return View(new UserRegistration());
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(UserRegistration model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.LoginProp };
                var createResult = await _userManager.CreateAsync(user, model.Password);

                if (createResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.user.Name);//присваивание пользователю роли user
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in createResult.Errors)
                    {
                        ModelState.AddModelError(String.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
